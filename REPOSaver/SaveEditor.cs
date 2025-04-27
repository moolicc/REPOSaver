using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using IDictionary = System.Collections.IDictionary;

namespace REPOSaver
{

    public partial class SaveEditor : Form
    {

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<string, object> Fields { get; private set; }



        private TreeNode Node_Global => treeView1.Nodes[0];
        private TreeNode Node_Global_Items => Node_Global.Nodes[0];
        private TreeNode Node_Global_Unknown => Node_Global.Nodes[1];
        private TreeNode Node_Players => treeView1.Nodes[1];
        private TreeNode Node_Unhandled => treeView1.Nodes[2];

        private Dictionary<long, TreeNode> _playerNodes;




        public SaveEditor(Dictionary<string, object> fields)
        {
            _playerNodes = new Dictionary<long, TreeNode>();
            Fields = fields;
            InitializeComponent();


            Node_Global.Tag = new List<DynamicProperty>();
            Node_Global_Items.Tag = new List<DynamicProperty>();
            Node_Global_Unknown.Tag = new List<DynamicProperty>();
            Node_Players.Tag = new List<DynamicProperty>();
            Node_Unhandled.Tag = new List<DynamicProperty>();
        }

        private void SaveEditor_Load(object sender, EventArgs e)
        {
            LoadGlobals();
            LoadPlayerIds();
            LoadDictionaryOfDictionaries();

            if (Node_Unhandled.Nodes.Count == 0)
            {
                treeView1.Nodes.Remove(Node_Unhandled);
            }

            treeView1.SelectedNode = Node_Global;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag == null)
            {
                propertyGrid1.SelectedObject = null;
            }
            else
            {
                propertyGrid1.SelectedObject = new DynamicPropertyGridAdapter(e.Node.Fields());
            }
        }


        private void Mnu_Reset_Opening(object sender, CancelEventArgs e)
        {
            if (propertyGrid1.SelectedObject == null)
            {
                resetToolStripMenuItem.Enabled = false;
                return;
            }

            PropertyDescriptor? pd = propertyGrid1.SelectedGridItem?.PropertyDescriptor;
            if (pd == null)
            {
                resetToolStripMenuItem.Enabled = false;
                return;
            }

            resetToolStripMenuItem.Enabled = pd.CanResetValue(propertyGrid1.SelectedObject);
        }

        private void resetValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            propertyGrid1.ResetSelectedProperty();
            return;


            PropertyDescriptor? pd = propertyGrid1.SelectedGridItem?.PropertyDescriptor;
            if (pd == null || propertyGrid1.SelectedObject == null)
            {
                return;
            }
            pd.ResetValue(propertyGrid1.SelectedObject);

        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Continuing will apply any changes made to the save file. Do so at your own risk!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                == DialogResult.Cancel)
            {
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


        private void LoadGlobals()
        {
            Node_Global.Fields().AddRange(
                [
                    CreateProperty("Save Name", "teamName",
                        isReadonly: true,
                        description: "The name of the save. This may be changed from the main screen by right-clicking the save file and selecting \"Rename\"."),

                    CreateProperty("Time Played", "timePlayed", description: "The total amount of time played on this save.")
                ]);

            foreach (var item in Fields)
            {
                if (item.Key == "teamName" || item.Key == "dictionaryOfDictionaries" || item.Key == "playerNames" || item.Key == "timePlayed")
                {
                    continue;
                }
                Node_Global.Fields().Add(CreateProperty(WithCapitals(item.Key), item.Key));
            }
        }

        private void LoadPlayerIds()
        {
            Dictionary<string, string> playerNames = (Dictionary<string, string>)Fields["playerNames"];
            foreach (var player in playerNames)
            {
                if (!long.TryParse(player.Key, out long id))
                {
                    continue;
                }

                Node_Players.Fields().Add(CreateProperty(player.Key, $"playerNames///{player.Key}", isReadonly: true));

                List<DynamicProperty> properties =
                    [
                        new DynamicProperty() {
                            Name = "Steam ID",
                            Description = $"The player's Steam account ID.",
                            IsReadOnly = true,
                            ValueType = typeof(long),
                            ValueGetter = (_) => id,
                            Category = "Player",
                        },
                        new DynamicProperty() {
                            Name = "Steam Name",
                            Description = "The player's Steam account name when they last played on this save.",
                            IsReadOnly = true,
                            ValueType = typeof(string),
                            ValueGetter = (_) => player.Value,
                            Category = "Player",
                        }
                    ];
                TreeNode node = new TreeNode(player.Value);
                node.Tag = properties;
                Node_Players.Nodes.Add(node);
                _playerNodes.Add(id, node);
            }
        }

        private void LoadDictionaryOfDictionaries()
        {
            Dictionary<string, Dictionary<string, int>> dictOfDicts = (Dictionary<string, Dictionary<string, int>>)Fields["dictionaryOfDictionaries"];

            foreach (var dict in dictOfDicts)
            {
                string basePath = $"dictionaryOfDictionaries///{dict.Key}";

                Dictionary<string, int> nested = dict.Value;
                if (dict.Key == "runStats")
                {
                    LoadRunStatus(nested);
                }
                else if (dict.Key.StartsWith("items") || dict.Key == "itemBatteryUpgrades")
                {
                    int prefixLength = 5;
                    if (dict.Key == "itemBatteryUpgrades")
                    {
                        prefixLength = 4;
                    }
                    LoadItemDictionary(5, nested, basePath, WithCapitals(dict.Key.Substring(prefixLength)));
                }
                else if (dict.Key.StartsWith("playerUpgrade"))
                {
                    LoadPlayerDictionary(nested, basePath, WithCapitals(dict.Key.Remove(0, 6)), "Upgrades");
                }
                else if (dict.Key.StartsWith("player"))
                {
                    LoadPlayerDictionary(nested, basePath, WithCapitals(dict.Key.Remove(0, 6)), "Player");
                }
                else if (dict.Key == "item" || dict.Key == "itemStatBattery")
                {
                    LoadIdkDictionary(nested, basePath, WithCapitals(dict.Key));
                }
                else
                {
                    LoadUnhandled(nested, basePath, dict.Key);
                }
            }
        }

        private void LoadRunStatus(Dictionary<string, int> stats)
        {
            foreach (var stat in stats)
            {
                string path = $"dictionaryOfDictionaries///runStats///{stat.Key}";
                Node_Global.Fields().Add(CreateProperty(WithCapitals(stat.Key), path, category: "Stats"));
            }
        }

        private void LoadItemDictionary(int prefixLength, Dictionary<string, int> values, string basePath, string category)
        {
            foreach (var item in values)
            {
                string path = $"{basePath}///{item.Key}";
                Node_Global_Items.Fields().Add(CreateProperty($"{WithCapitals(item.Key.Remove(0, prefixLength))} {category}", path, category));
            }
        }

        private void LoadIdkDictionary(Dictionary<string, int> values, string basePath, string category)
        {
            foreach (var item in values)
            {
                string path = $"{basePath}///{item.Key}";
                Node_Global_Unknown.Fields().Add(CreateProperty(WithCapitals(item.Key), path, category));
            }
        }

        private void LoadPlayerDictionary(Dictionary<string, int> values, string basePath, string name, string category)
        {
            foreach (var playerSet in values)
            {
                if (long.TryParse(playerSet.Key, out long id))
                {
                    string path = $"{basePath}///{playerSet.Key}";
                    TreeNode node = _playerNodes[id];
                    node.Fields().Add(CreateProperty(name, path, category));
                }
            }
        }

        private void LoadUnhandled(Dictionary<string, int> values, string basePath, string category)
        {
            foreach (var value in values)
            {
                string path = $"{basePath}///{value.Key}";
                Node_Unhandled.Fields().Add(CreateProperty(value.Key, path, category));
            }
        }

        public string WithCapitals(string input)
        {
            StringBuilder newString = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                if (i == 0)
                {
                    newString.Append(char.ToUpper(input[i]));
                }
                else if (char.IsUpper(input[i]) && i > 0)
                {
                    newString.Append(' ').Append(input[i]);
                }
                else if (input[i] == ' ')
                {
                    newString.Append(' ');
                    if (i + 1 < input.Length)
                    {
                        newString.Append(char.ToUpper(input[i + 1]));
                        i++;
                    }
                }
                else
                {
                    newString.Append(input[i]);
                }
            }

            return newString.ToString();
        }

        private DynamicProperty CreateProperty(string name, string fieldPath, string category = "", string description = "", bool isReadonly = false)
        {
            if (string.IsNullOrEmpty(description))
                description = $"JSON Path: {fieldPath}";

            return new DynamicProperty(GetValue(fieldPath)?.GetType() ?? typeof(object))
            {
                Category = category,
                Description = description,
                IsReadOnly = isReadonly,
                Name = name,
                ValueType = GetValue(fieldPath)?.GetType() ?? typeof(object),
                ValueGetter = (_) => GetValue(fieldPath),
                ValueSetter = (_, v) => SetValue(fieldPath, v),
            };
        }

        private object? GetValue(string path, int arrayIndex = -1)
        {
            string[] parts = path.Split("///");
            IDictionary cur = Fields;

            for (int i = 0; i < parts.Length - 1; i++)
            {
                string part = parts[i];

                object nextDict = cur[part];
                if (nextDict is IDictionary nested)
                {
                    cur = nested;
                }
            }

            object? value = cur[parts[parts.Length - 1]];

            if (arrayIndex != -1)
            {
                string arrayString = value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(arrayString))
                {
                    // Remove square brackets.
                    arrayString.Remove(0, 1).Remove(arrayString.Length - 1, 1);

                    string[] elements = arrayString.Split(',');
                    value = elements[arrayIndex].Trim();
                }
            }

            return value;
        }


        private void SetValue(string path, object value)
        {
            string[] parts = path.Split("///");
            int pathIndex = 0;

            IDictionary cur = Fields;

            for (int i = 0; i < parts.Length - 1; i++)
            {
                string part = parts[i];

                object nextDict = cur[part]!;
                if (nextDict is IDictionary nested)
                {
                    cur = nested;
                }
            }

            cur[parts[parts.Length - 1]] = value;
        }
    }
}

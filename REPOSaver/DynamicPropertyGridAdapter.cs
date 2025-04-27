using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSaver
{
    class DynamicProperty
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsReadOnly { get; set; }
        public Type ValueType { get; init; }


        public Func<DynamicProperty, object?> ValueGetter { get; init; }
        public Action<DynamicProperty, object?> ValueSetter { get; init; }

        public DynamicProperty()
        {
            Name = "";
            Description = "";
            Category = "";
            ValueType = typeof(object);
            ValueGetter = (_) => "";
            ValueSetter = (_, _) => { };
        }

        public DynamicProperty(Type type)
        {
            Name = "";
            Description = "";
            Category = "";
            ValueType = type;
            ValueGetter = (_) => "";
            ValueSetter = (_, _) => { };
        }
    }


    class DynamicPropertyGridAdapter : ICustomTypeDescriptor
    {
        public List<DynamicProperty> Properties { get; private set; }

        public DynamicPropertyGridAdapter()
        {
            Properties = new List<DynamicProperty>();
        }

        public DynamicPropertyGridAdapter(params DynamicProperty[] properties)
        {
            Properties = [.. properties];
        }

        public DynamicPropertyGridAdapter(IEnumerable<DynamicProperty> properties)
        {
            Properties = [.. properties];
        }


        // Note: These next three functions are supposedly unused by the propertygrid.
        public string? GetClassName()
        {
            return nameof(DynamicPropertyGridAdapter);
        }

        public string? GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this);
        }

        public EventDescriptor? GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this);
        }




        public TypeConverter? GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }


        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public PropertyDescriptor? GetDefaultProperty()
        {
            return null;
        }

        public object? GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[]? attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public object? GetPropertyOwner(PropertyDescriptor? pd)
        {
            return Properties;
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return ((ICustomTypeDescriptor)this).GetProperties(Array.Empty<Attribute>());
        }

        public PropertyDescriptorCollection GetProperties(Attribute[]? attributes)
        {
            PropertyDescriptor[] props = Properties.Select(p => new DynamicPropertyDescriptor(p)).ToArray();
            return new PropertyDescriptorCollection(props);
        }
    }

    class DynamicPropertyDescriptor : PropertyDescriptor
    {
        public DynamicProperty Property { get; private set; }

        public override Type ComponentType => PropertyType;
        public override Type PropertyType { get; }
        public override bool IsReadOnly => Property.IsReadOnly;

        public object? DefaultValue { get; private set; }

        public override AttributeCollection Attributes
            => new AttributeCollection([new CategoryAttribute(Property.Category), new DisplayNameAttribute(Property.Name), new DescriptionAttribute(Property.Description), new DefaultValueAttribute(DefaultValue)]);



        public DynamicPropertyDescriptor(DynamicProperty property)
            : base(property.Name, null)
        {
            Property = property;
            PropertyType = property.ValueType;
            DefaultValue = GetValue(null);
        }


        public override object? GetValue(object? component)
        {
            return Property.ValueGetter(Property);
        }

        public override void SetValue(object? component, object? value)
        {
            Property.ValueSetter(Property, value);
            OnValueChanged(component, EventArgs.Empty);
        }


        public override bool CanResetValue(object component)
        {
            object? curValue = GetValue(component);
            return !object.Equals(curValue, DefaultValue);
        }

        public override void ResetValue(object component)
        {
            SetValue(component, DefaultValue);
            OnValueChanged(component, EventArgs.Empty);
        }

        public override bool ShouldSerializeValue(object component)
        {
            object? curValue = GetValue(component);
            return !object.Equals(curValue, DefaultValue);
        }
    }
}

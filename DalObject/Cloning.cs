//Yehonatan Eliyahu 311555387
//Mendi Shneorson 204290688
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dal

{/// <summary>
/// Clonig func  as extention method to all types 
/// 
/// </summary>
    static class Cloning
    {
        internal static T Clone<T>(this T original)
        {
               //create instance of our type using reflection
            T target = (T)Activator.CreateInstance(original.GetType());
            var Fields = original.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance|BindingFlags.Static);
            foreach (var field in Fields)
            {
                //check if its valuetype or string
                if (field.FieldType.IsValueType || field.FieldType.Equals(typeof(string)))
                {
                    field.SetValue(target, field.GetValue(original));
                }
                //if is refernce type or Array  types 
                else
                {
                    if (field.FieldType.IsArray&& field.FieldType.GetElementType().IsValueType)
                    {
                              //now we can clone every array given
                        Array array =(Array) field.GetValue(original);
                        if (array != null)
                            field.SetValue(target, array.Clone());
                        else
                            field.SetValue(target, null);
                    }
                    else
                    {
                        //if our referencetypr is null we dont need to clone it , we just init it with null
                        if (field.GetValue(original) == null)
                        {
                            field.SetValue(target, null);
                        }
                        else
                        {
                                   //now we know its a  initilized refrence type  now we clone it using recursive
                            field.SetValue(target, field.GetValue(original).Clone());
                        }
                    }
                }
            }
            //return our new cloned item
            return target;
        }
    }
}

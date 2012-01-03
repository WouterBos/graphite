using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Web;

/// <summary>
/// This class implements methods that convert string representation of Dictionary
/// to Dictionary object and vice versa - converter will convert only dictionaries
/// where both key and value are of type string
/// </summary>
public class SpotlightRotatorItemListConverter : TypeConverter
{
    /// <summary>
    /// Returns whether this converter can convert an object of the given type to
    /// the type of this converter, using the specified context.
    /// </summary>
    /// <param name="context">A System.ComponentModel.ITypeDescriptorContext that provides a format context.</param>
    /// <param name="sourceType">A System.Type that represents the type you want to convert from.</param>
    /// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return ((sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType));
    }
    
    /// <summary>
    /// Returns whether this converter can convert the object to the specified type,
    /// using the specified context.
    /// </summary>
    /// <param name="context">A System.ComponentModel.ITypeDescriptorContext that provides a format context.</param>
    /// <param name="destinationType">A System.Type that represents the type you want to convert to.</param>
    /// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        return ((destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType));
    }
    
    /// <summary>
    /// Converts the given object to the type of this converter, using the specified
    /// context and culture information.
    /// </summary>
    /// <param name="context">A System.ComponentModel.ITypeDescriptorContext that provides a format context.</param>
    /// <param name="culture">The System.Globalization.CultureInfo to use as the current culture.</param>
    /// <param name="value">The System.Object to convert.</param>
    /// <returns>An System.Object that represents the converted value.</returns>
    /// <exception cref="System.NotSupportedException">The conversion cannot be performed.</exception>
    public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
    {
        // the logic in this method will convert the xml string into a list<SpotlightRotatoritem>
        if (value is string)
        {
            List<SpotlightRotatorItem> list = new List<SpotlightRotatorItem>();
            
            string inputString = (string)value;
            if (!string.IsNullOrEmpty(inputString))
            {
                int itemCnt = 0;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(inputString);
                XPathNavigator nav = xmlDoc.CreateNavigator();
                XPathExpression expr = nav.Compile("/items/item");
                XPathNodeIterator iterator = nav.Select(expr);
                
                while (iterator.MoveNext())
                {
                    XPathNavigator itemTitle = iterator.Current.SelectSingleNode("title");
                    XPathNavigator itemSubtitle = iterator.Current.SelectSingleNode("subtitle");
                    XPathNavigator itemSummary = iterator.Current.SelectSingleNode("summary");
                    XPathNavigator itemUrl = iterator.Current.SelectSingleNode("url");
                    XPathNavigator itemLinktext = iterator.Current.SelectSingleNode("linktext");
                    XPathNavigator itemImage = iterator.Current.SelectSingleNode("image");

                    list.Add(new SpotlightRotatorItem(itemCnt, HttpUtility.HtmlDecode(itemTitle.Value), HttpUtility.HtmlDecode(itemSubtitle.Value), HttpUtility.HtmlDecode(itemSummary.Value), itemUrl.Value, HttpUtility.HtmlDecode(itemLinktext.Value), itemImage.Value));
                    
                    itemCnt++;
                }
            }

            return list;                
        }
        return base.ConvertFrom(context, culture, value);
    }
    
    /// <summary>
    /// Converts the given value object to the specified type, using the specified
    ///     context and culture information.
    /// </summary>
    /// <param name="context">A System.ComponentModel.ITypeDescriptorContext that provides a format context.</param>
    /// <param name="culture">A System.Globalization.CultureInfo. If null is passed, the current culture is assumed.</param>
    /// <param name="value">The System.Object to convert.</param>
    /// <param name="destinationType">The System.Type to convert the value parameter to.</param>
    /// <returns>An System.Object that represents the converted value.</returns>
    /// <exception cref="System.NotSupportedException">The conversion cannot be performed.</exception>
    /// <exception cref="System.ArgumentNullException">The destinationType parameter is null.</exception>
    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
        // the logic in this method will convert the list<SpotlightRotatoritem> into a xml string
        if (destinationType == null)
        {
            throw new ArgumentNullException("destinationType");
        }
        
        if ((destinationType == typeof(string)) && (value is List<SpotlightRotatorItem>))
        {
            string outputString = string.Empty;
            List<SpotlightRotatorItem> items = (List<SpotlightRotatorItem>)value;
            if (items != null || items.Count > 0)
            {
                string itemXml = "<item><title><![CDATA[{0}]]></title><subtitle><![CDATA[{1}]]></subtitle><summary><![CDATA[{2}]]></summary><url><![CDATA[{3}]]></url><linktext><![CDATA[{4}]]></linktext><image><![CDATA[{5}]]></image></item>\r\n";
                string itemsXml = string.Empty;
                foreach (SpotlightRotatorItem item in items)
                {
                    string itemTitle = HttpUtility.HtmlEncode(item.Title);
                    string itemSubtitle = HttpUtility.HtmlEncode(item.Subtitle);
                    string itemSummary = HttpUtility.HtmlEncode(item.Summary);
                    string itemLinktext = HttpUtility.HtmlEncode(item.Linktext);
                    
                    itemsXml += string.Format(itemXml, itemTitle, itemSubtitle, itemSummary, item.Url, itemLinktext, item.Image);
                }
                
                string itemsListXml = string.Format("<?xml version=\"1.0\" encoding= \"utf-8\" ?>\r\n<items>\r\n{0}</items>\r\n", itemsXml);
                outputString = itemsListXml;
            }
            
            return outputString;
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }
}

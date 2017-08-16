namespace Spectrum.Content.ContentModels
{
    using System;
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;

    public class BaseModel : PublishedContentModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Umbraco.Core.Models.PublishedContent.PublishedContentModel" /> class with
        /// an original <see cref="T:Umbraco.Core.Models.IPublishedContent" /> instance.
        /// </summary>
        /// <param name="content">The original content.</param>
        public BaseModel(IPublishedContent content) : base(content)
        {
        }

        /// <summary>
        /// Gets the published property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public IPublishedProperty GetPublishedProperty(string propertyName)
        {
            return GetProperty(propertyName);
        }

        /// <summary>
        /// Gets the node identifier.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public int? GetNodeId(string propertyName)
        {
            IPublishedProperty property = GetPublishedProperty(propertyName);

            if (property != null &&
                property.HasValue)
            {
                int value;
                int? nodeId = null;

                bool isNumeric = int.TryParse(property.DataValue.ToString(), out value);

                if (isNumeric)
                {
                    nodeId = value;
                }

                else
                {
                    string propertyValue = property.Value.ToString();

                    isNumeric = int.TryParse(propertyValue, out value);

                    if (isNumeric)
                    {
                        nodeId = value;
                    }

                    else
                    {
                        Type type = property.Value.GetType();

                        if (type == typeof(GuidUdi))
                        {
                            GuidUdi guid = property.Value as GuidUdi;

                        }
                    } 
                }

                return nodeId;
            }

            return null;
        }

        /// <summary>
        /// Gets the nice URL.
        /// </summary>
        /// <param name="nodeId">The node identifier.</param>
        /// <returns></returns>
        public string GetNiceUrl(int? nodeId)
        {
            if (nodeId.HasValue)
            {
                return umbraco.library.NiceUrl(nodeId.Value);
            }

            return string.Empty;
        }
    }
}

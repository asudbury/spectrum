namespace Spectrum.Content.ContentModels
{
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
        /// Gets the node identifier.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public int? GetNodeId(string propertyName)
        {
            IPublishedProperty property = GetProperty(propertyName);

            if (property != null)
            {
                int nodeId = (int)property.DataValue;

                return nodeId;
            }

            return null;
        }

        /// <summary>
        /// Gets the nice URL.
        /// </summary>
        /// <param name="nodeId">The node identifier.</param>
        /// <returns></returns>
        public string GetNiceUrl(int nodeId)
        {
            return umbraco.library.NiceUrl(nodeId);
        }
    }
}

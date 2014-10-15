using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSlackers.Localization
{
    /// <summary>
    /// Specifies where to find a resource by giving the type an name of the resource.
    /// </summary>
    internal class ResourceLocation
    {
        public Type ResourceType { get; set; }
        public string ResourceName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ElementDefinitionAttribute : Attribute
    {
        public ElementDefinitionAttribute(params string[] path)
        {
            this.path = path;

            this.locator = 0;

            this.mode = ElementMode.Strict;
        }

        public ElementDefinitionAttribute(int locator, params string[] path)
        {
            this.path = path;

            this.locator = locator;

            this.mode = ElementMode.Strict;
        }

        public ElementDefinitionAttribute(ElementMode mode, params string[] path)
        {
            this.path = path;
            this.locator = 0;
            this.mode = mode;
        }

        public ElementDefinitionAttribute(ElementMode mode, int locator, params string[] path)
        {
            this.path = path;
            this.locator = locator;
            this.mode = mode;
        }

        public ElementMode mode { get; set; }

        public string[] path { get; set; }

        public int locator { get; set; }
    }


    public enum ElementMode
    {
        Strict,
        Soft,
        Lazy
    }
}

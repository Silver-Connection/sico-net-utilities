namespace SiCo.Utilities.Web.Charts.ChartJs.Base
{
    using System.Linq;

    public class XModelBase : DatasetBaseModel<decimal>, IXModel
    {
        public XModelBase() : base()
        {
        }

        public XModelBase(string label) : base(label)
        {
        }

        public override void AddValue(decimal value)
        {
            this.Data.Add(value);
        }

        public override void AddValue(decimal value, string color)
        {
            this.Data.Add(value);
        }

        public override void CloneValue()
        {
            if (this.Data?.Count > 0)
            {
                this.Data.Add(this.Data.Last());
            }
        }
    }
}
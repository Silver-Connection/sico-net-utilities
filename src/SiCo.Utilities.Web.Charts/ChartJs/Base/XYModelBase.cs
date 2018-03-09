namespace SiCo.Utilities.Web.Charts.ChartJs.Base
{
    using System;

    public class XYModelBase : DatasetBaseModel<Base.DataPointXYModel<string, decimal>>, IXYModel
    {
        public XYModelBase() : base()
        {
        }

        public XYModelBase(string label) : base(label)
        {
        }

        public virtual void AddValue(string x, decimal y)
        {
            this.Data.Add(new Base.DataPointXYModel<string, decimal>()
            {
                X = x,
                Y = Math.Round(y, 2, MidpointRounding.AwayFromZero),
            });
        }

        public override void AddValue(Base.DataPointXYModel<string, decimal> value, string color)
        {
            this.Data.Add(value);
        }

        public virtual void AddValue(string x, decimal y, string color)
        {
            this.Data.Add(new Base.DataPointXYModel<string, decimal>()
            {
                X = x,
                Y = Math.Round(y, 2, MidpointRounding.AwayFromZero),
            });
        }
    }
}
using System;
using System.ServiceModel.Configuration;

namespace Service.InterbankTransfer.Error
{
    public class JsonErrorWebHttpBehaviorElelement : BehaviorExtensionElement
    {
        public override Type BehaviorType => typeof(JsonErrorWebHttpBehavior);

        protected override object CreateBehavior() => new JsonErrorWebHttpBehavior();
    }
}
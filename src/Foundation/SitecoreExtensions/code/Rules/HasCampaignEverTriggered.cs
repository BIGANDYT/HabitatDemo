using Sitecore.Analytics;
using Sitecore.Diagnostics;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;
using System;
using System.Linq;

namespace Sitecore.Foundation.SitecoreExtensions.Rules
{
    public class HasCampaignEverTriggered<T> : WhenCondition<T> where T : RuleContext
    {
        private const string CamapignTempalteId = "{F358D040-256F-4FC6-B2A1-739ACA2B2983}";
        private Guid CampaignGuid { get; set; }
        public string CampaignId { get; set; }
        protected override bool Execute(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext");
            try
            {
                this.CampaignGuid = new Guid(this.CampaignId);
            }
            catch
            {
                Log.Warn(string.Format("Could not convert value to guid: {0}", this.CampaignId), base.GetType()); return false;
            }
            return Tracker.Current.Session.Interaction.CurrentPage.PageEvents.Any(row => row.PageEventDefinitionId == new Guid(CamapignTempalteId) && row.Data == this.CampaignId);
        }
    }
}


﻿using TravelAgencyBusinessLogic.Interfaces;

namespace TravelAgencyBusinessLogic.HelperModels.Guarantor
{
    public class MailCheckInfo
    {
        public string PopHost { get; set; }
        public int PopPort { get; set; }
        public IMessageInfoStorage MessageStorage { get; set; }
        public IOrganizatorStorage OrganizatorStorage { get; set; }
    }
}

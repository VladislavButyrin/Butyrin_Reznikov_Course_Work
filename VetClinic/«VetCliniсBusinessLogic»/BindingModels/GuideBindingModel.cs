using System.Collections.Generic;

namespace _VetCliniсBusinessLogic_.BindingModels
{
    /// <summary>
    /// Лекарство, которое основано на медикаментах, предаставляемое ветеринарной клиникой
    /// </summary>
    public class GuideBindingModel
    {
        public int? Id { get; set; }
        public string GuideName { get; set; }
        public int Cost { get; set; }
        public Dictionary<int, string> Trips { get; set; }
    }
}

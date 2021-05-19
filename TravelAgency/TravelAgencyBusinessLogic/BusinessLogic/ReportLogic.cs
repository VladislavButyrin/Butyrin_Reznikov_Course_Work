using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.HelperModels;
using _VetCliniсBusinessLogic_.Interfaces;
using _VetCliniсBusinessLogic_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _VetCliniсBusinessLogic_.BusinessLogic
{
    public class ReportLogic
    {
        private readonly IMedicationStorage _medicationStorage;
        private readonly IMedicineStorage _medicineStorage;
        private readonly IVisitStorage _visitStorage;
        private readonly IPurchaseStorage _purchaseStorage;
        public ReportLogic(IMedicationStorage medicationStorage, IMedicineStorage
       medicineStorage, IVisitStorage visitStorage, IPurchaseStorage purchaseStorage)
        {
            _medicationStorage = medicationStorage;
            _medicineStorage = medicineStorage;
            _visitStorage = visitStorage;
            _purchaseStorage = purchaseStorage;
        }
        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        public List<ReportPurchaseMedicationViewModel> GetPurchasesMedication(ReportBindingModel model)
        {
            var medications = _medicationStorage.GetFullList();
            var purchases = _purchaseStorage.GetFullList();
            var medicines = _medicineStorage.GetFullList();
            var list = new List<ReportPurchaseMedicationViewModel>();
            foreach (var purchase in purchases)
            {
                bool have_medications = true;
                foreach (int medication in model.Medications) 
                {
                    foreach (var medicine in purchase.MedicinesPurchases)
                    {
                        if (!medicines.FirstOrDefault(rec => rec.Id == medicine.Key).Medications.ContainsKey(medication))
                        {
                            have_medications = false;
                        }
                    }
                }
                if (!have_medications)
                    continue;
                var record = new ReportPurchaseMedicationViewModel
                {
                    PurchaseId = purchase.Id,
                    Date = purchase.DatePayment,
                    Sum = purchase.Sum,
                };
                list.Add(record);
            }
            return list;
        }
        public List<ReportServiceMedicineViewModel> GetServiceMedicine(ReportBindingModel model)
        {
            var purchaces = _purchaseStorage.GetFilteredList(new PurchaseBindingModel { 
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });
            var visits = _visitStorage.GetFilteredList(new VisitBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });
            var list = new List<ReportServiceMedicineViewModel>();
            foreach (var purchace in purchaces)
            {
                foreach (var medicine in purchace.MedicinesPurchases)
                {
                    var selectedvisits = visits.Where(rec => rec.DateVisit.Date == purchace.DatePayment.Date).ToList();
                    foreach (var visit in selectedvisits)
                    {
                        var report = visit.ServicesVisits.Values.Select(s => new ReportServiceMedicineViewModel
                        {
                            Date = visit.DateVisit.Date,
                            ServiceName = s,
                            MedicineName = medicine.Value.Item1,
                            MedicineCount = medicine.Value.Item2
                        }).ToList();
                        report.ForEach(rep => list.Add(rep));
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SavePurchasesToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordExelInfo
            {
                FileName = model.FileName,
                Title = "Список покупок на основе выбранных медикаментов",
                MedicineMedications = GetPurchasesMedication(model),
                NeededMedications = _medicationStorage.GetFullList().Where(rec => model.Medications.Contains(rec.Id)).Select(rec => rec.MedicationName).ToList()
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SavePurchasesToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new WordExelInfo
            {
                FileName = model.FileName,
                Title = "Список покупок на основе выбранных медикаментов",
                MedicineMedications = GetPurchasesMedication(model),
                NeededMedications = _medicationStorage.GetFullList().Where(rec => model.Medications.Contains(rec.Id)).Select(rec => rec.MedicationName).ToList()
            });
        }
        public void SaveOrderToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            { 
                FileName = model.FileName,
                DateFrom = (DateTime)model.DateFrom,
                DateTo = (DateTime)model.DateTo,
                Title = "Отчёт",
                ServicesMedicines = GetServiceMedicine(model)
            });
        }
    }
}

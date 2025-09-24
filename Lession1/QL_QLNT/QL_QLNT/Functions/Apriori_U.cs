using Accord.MachineLearning.Rules;
using System.Collections.Generic;
using Web_QLNT.Models;

namespace Web_QLNT.Functions
{
    public class Apriori_U
    {
        public Apriori_U()
        {
        }

        public AssociationRule<string>[] DoApriori(List<CToHD> ct)
        {
            // Kiểm tra dữ liệu đầu vào
            if (ct == null || ct.Count == 0)
            {
                return new AssociationRule<string>[0];
            }

            // Tạo dataset từ CToHD
            var dataset = new List<string[]>();

            foreach (CToHD item in ct)
            {
                if (item?.Ds != null && item.Ds.Count > 0)
                {
                    var transaction = new List<string>();
                    foreach (var cthd in item.Ds)
                    {
                        if (!string.IsNullOrEmpty(cthd.MaSP))
                        {
                            transaction.Add(cthd.MaSP);
                        }
                    }
                    if (transaction.Count > 0)
                    {
                        dataset.Add(transaction.ToArray());
                    }
                }
            }

            // Kiểm tra nếu dataset rỗng
            if (dataset.Count == 0)
            {
                return new AssociationRule<string>[0];
            }

            try
            {
                // Create a new A-priori learning algorithm
                var apriori = new Apriori<string>(threshold: 2, confidence: 0.5); // Giảm threshold để có kết quả

                // Use apriori to generate frequent patterns
                AssociationRuleMatcher<string> classifier = apriori.Learn(dataset.ToArray());

                // Generate association rules from the itemsets
                AssociationRule<string>[] rules = classifier.Rules;

                return rules ?? new AssociationRule<string>[0];
            }
            catch
            {
                return new AssociationRule<string>[0];
            }
        }

        // Phương thức overload để xử lý trực tiếp từ danh sách hóa đơn
        public AssociationRule<string>[] DoAprioriFromHoaDons(List<HoaDon> hoaDons)
        {
            if (hoaDons == null || hoaDons.Count == 0)
            {
                return new AssociationRule<string>[0];
            }

            var dataset = new List<string[]>();

            foreach (var hoaDon in hoaDons)
            {
                if (hoaDon?.CTHoaDons != null && hoaDon.CTHoaDons.Count > 0)
                {
                    var transaction = hoaDon.CTHoaDons
                        .Where(ct => !string.IsNullOrEmpty(ct.MaSP))
                        .Select(ct => ct.MaSP)
                        .ToArray();

                    if (transaction.Length > 0)
                    {
                        dataset.Add(transaction);
                    }
                }
            }

            return DoAprioriFromDataset(dataset);
        }

        // Phương thức hỗ trợ xử lý dataset trực tiếp
        private AssociationRule<string>[] DoAprioriFromDataset(List<string[]> dataset)
        {
            if (dataset == null || dataset.Count == 0)
            {
                return new AssociationRule<string>[0];
            }

            try
            {
                var apriori = new Apriori<string>(threshold: 2, confidence: 0.5);
                AssociationRuleMatcher<string> classifier = apriori.Learn(dataset.ToArray());
                return classifier.Rules ?? new AssociationRule<string>[0];
            }
            catch
            {
                return new AssociationRule<string>[0];
            }
        }
    }
}
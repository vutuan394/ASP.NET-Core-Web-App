namespace Web_QLNT.Functions
{
    public class GetAddress
    {
        public static string GetFullAddress(string soNha, string duong, string phuong, string quan, string thanhpho)
        {
            var addressParts = new List<string>();

            if (!string.IsNullOrWhiteSpace(soNha))
                addressParts.Add(soNha.Trim());

            if (!string.IsNullOrWhiteSpace(duong))
                addressParts.Add(duong.Trim());

            if (!string.IsNullOrWhiteSpace(phuong))
                addressParts.Add(phuong.Trim());

            if (!string.IsNullOrWhiteSpace(quan))
                addressParts.Add(quan.Trim());

            if (!string.IsNullOrWhiteSpace(thanhpho))
                addressParts.Add(thanhpho.Trim());

            // Loại bỏ các phần tử rỗng và nối bằng dấu phẩy
            return string.Join(", ", addressParts.Where(part => !string.IsNullOrEmpty(part)));
        }

        public static string GetShortAddress(string phuong, string quan, string thanhpho)
        {
            var addressParts = new List<string>();

            if (!string.IsNullOrWhiteSpace(phuong))
                addressParts.Add(phuong.Trim());

            if (!string.IsNullOrWhiteSpace(quan))
                addressParts.Add(quan.Trim());

            if (!string.IsNullOrWhiteSpace(thanhpho))
                addressParts.Add(thanhpho.Trim());

            return string.Join(", ", addressParts.Where(part => !string.IsNullOrEmpty(part)));
        }
    }
}
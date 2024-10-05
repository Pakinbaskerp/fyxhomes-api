namespace API.Data.Dto
{
    public class CategoryDto
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public int SortOrder { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
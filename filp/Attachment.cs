namespace filp
{
    public class Attachment
    {
        public string Title { get; set; } = string.Empty;
        public string Text  { get; set; } = string.Empty;
        public int    Pages { get; set; } = 1;

        public override string ToString() =>
            string.IsNullOrWhiteSpace(Title) ? "(без названия)" : Title;
    }
}

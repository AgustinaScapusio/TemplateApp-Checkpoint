namespace Template
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TemplateClass temp = new();

            List<TemplateClass> tempList = new();
            foreach (var item in temp) {
                Console.WriteLine(temp.CreateOneRowInDB(item.n2, item.n3, item.n4)); 
            }
        }
    }
}

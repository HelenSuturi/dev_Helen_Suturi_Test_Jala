using System.Collections.Generic;

namespace Assessment
{
    public class StringProvider : IElementsProvider<string>
    {
        private readonly string separatora = ",";
        private readonly string separatorb = "|";
        private readonly string separatorc = " ";

        public IEnumerable<string> ProcessData(string source,string option)
        {
            string separated = "";
            if(option == "1")
                separated = separatora;
            if(option == "2")
                separated = separatorb;
            if(option == "3")
                separated = separatorc;

            return source.Split(separated);
        }
    }
}
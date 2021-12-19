using System.Collections.Generic;

namespace UnitTest
{
    public class FileValidationResponse
    {
        public bool fileValid { get; set; }
        public List<string> invalidLines { get; set; }
    }
}

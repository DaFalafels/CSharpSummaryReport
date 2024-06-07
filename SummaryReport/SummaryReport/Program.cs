using System.Collections;

namespace SummaryReport
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "../CodeChallenge.csv";

            if (File.Exists(path)) {
                StreamReader reader = new StreamReader(File.OpenRead(path));
                bool checkHeader    = true;
                var salesPersons    = new Dictionary<string, float>();
                var regions         = new Dictionary<string, Dictionary<string, float>>();
                var regionDates     = new Dictionary<string, ArrayList>();

                while (!reader.EndOfStream) {
                    string line = reader.ReadLine();

                    if (checkHeader == false) {
                        string[] parsedLine = line.Split(',');
                        string currentSalesPerson   = "";
                        string currentRegion        = "";
                        string currentProduct       = "";
                        DateTime currentDate        = new DateTime();
                        int quantity                = 0;
                        float price                 = 0.0f;
                        float totalSales            = 0.0f;

                        for (int part = 0; part < parsedLine.Length; part++) {
                            switch (part)
                            {
                                case 0:           
                                    var isValidDate = DateTime.TryParse(parsedLine[part], out currentDate);

                                    if (!isValidDate) {
                                        Console.WriteLine($"'{parsedLine[part]}' is not a valid date string. Please add a properly-formatted date.");
                                        return;
                                    } 
                                        
                                    break;
    
                                case 1:
                                    if (parsedLine[part] == "") {
                                        Console.WriteLine("There is no name attached to this line! Please add a name.");
                                        return;
                                    } 

                                    if (!salesPersons.ContainsKey(parsedLine[part]))
                                        salesPersons.Add(parsedLine[part], 0.0f);
                                    
                                    currentSalesPerson = parsedLine[part];

                                    break;

                                case 2:
                                    if (parsedLine[part] == "") {
                                        Console.WriteLine("There is no region attached to this line! Please add a region.");
                                        return;
                                    }    

                                    if (!regions.ContainsKey(parsedLine[part]))
                                        regions.Add(parsedLine[part], new Dictionary<string, float>());

                                    if (!regionDates.ContainsKey(parsedLine[part]))
                                        regionDates.Add(parsedLine[part], new ArrayList());

                                    regionDates[parsedLine[part]].Add(currentDate);

                                    currentRegion = parsedLine[part];

                                    break;

                                case 3:
                                    if (parsedLine[part] == "") {
                                        Console.WriteLine("There is no product attached to this line! Please add a product.");
                                        return;
                                    }

                                    if (!regions[currentRegion].ContainsKey(parsedLine[part]))
                                        regions[currentRegion].Add(parsedLine[part], 0.0f);

                                    currentProduct = parsedLine[part];

                                    break;

                                case 4:
                                    var isValidInt = int.TryParse(parsedLine[part], out quantity);
                                    
                                    if (!isValidInt) {
                                        Console.WriteLine($"'{parsedLine[part]}' is not a valid integer string. Please add an whole number.");
                                        return;
                                    }

                                    break;

                                case 5:
                                    var isValidfloat = float.TryParse(parsedLine[part], out price);

                                    if (!isValidfloat) {
                                        Console.WriteLine($"'{parsedLine[part]}' is not a valid float string. Please add a number.");
                                        return;
                                    }

                                    totalSales = quantity * price;

                                    salesPersons[currentSalesPerson] += totalSales;

                                    regions[currentRegion][currentProduct] += totalSales;
                                    
                                    break;

                            }
                        }
                    } else if (line != "Date,SalesPerson,Region,Product,Quantity,Price") {
                        Console.WriteLine("The header of the file is of incorrect format. Please correct it to be like this: 'Date,SalesPerson,Region,Product,Quantity,Price'");
                        return;

                    } else 
                        checkHeader = false;
                }

            // Solution to Problem #1
            Console.WriteLine("Total Sales per Salesperson: ");
            foreach (var salesPerson in salesPersons)
                Console.WriteLine("- {0}: ${1}", salesPerson.Key, salesPerson.Value);

            Console.WriteLine(System.Environment.NewLine);

            // Solution to Problem #2
            Console.WriteLine("Top-Selling Product in Each Region: ");
            foreach (var region in regions) {
                string topProduct   = "";
                float topSales      = 0.0f;

                foreach(var product in region.Value) {
                    if (topSales < product.Value) {
                        topProduct  = product.Key;
                        topSales    = product.Value;
                    }
                } 
            
                Console.WriteLine("- {0}: {1}", region.Key, topProduct);
            }

            Console.WriteLine(System.Environment.NewLine);

            // Solution to Problem #3
            Console.WriteLine("Average Sales per Day in Each Region: ");
            foreach (var regionDate in regionDates)          
                Console.WriteLine("- {0}: {1}", regionDate.Key, regionDate.Value.Count);
            
            } else {
                Console.WriteLine("The file does not exist! Please place the file in the proper path: '../CodeChallenge.csv'");
                return;
            }
        }
    }
}
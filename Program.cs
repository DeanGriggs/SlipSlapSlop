using System;

namespace Slop
{
    public class SlopChecker
    {
        public static bool IsSlip(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false; // Empty or null string is not a slip
            }

            if (input[0] != 'D' && input[0] != 'E')
            {
                return false; // First character must be 'D' or 'E'
            }
            if (input[1] != 'F')
            { 
                return false; //Second character must be 'F'
            }
            int index = 1;
            
            while (index < input.Length && input[index] == 'F')
            {
                index++; // Skip consecutive 'F' characters
            }

           // while (index < input.Length)
           // {
           //     index++; 
           // }

            index--;
            if (index >= input.Length)
            {
                return false; // Slip must contain at least one 'F' followed by more characters
            }
            if (input[index - 1] != 'F')
            {
                return false;
            }
            if (input[index] == 'G')
            {
                return true; // Slip ends with 'G'
            }
            
            
            // Recursively check if the remaining part of the string is a slip
            string remaining = input.Substring(index);
            return IsSlip(remaining);
        }

        public static bool IsSlap(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false; // Empty or null string is not a slap
            }

            if (input[0] != 'A')
            {
                return false; // First character must be 'A'
            }

            if (input.Length == 2 && input[1] == 'H')
            {
                return true; // Two-character slap ends with 'H'
            }

            if (input.Length >= 4 && input[1] == 'B' && input[input.Length - 2] == 'C')
            {
                string middlePart = input.Substring(2, input.Length - 4);

                if (IsSlap(middlePart))
                {
                    return true; // Slap in the form of 'A' followed by 'B' followed by a slap, followed by 'C'
                }
            }

            if (input.Length >= 3 && input[input.Length - 2] == 'C')
            {
                string middlePart = input.Substring(1, input.Length - 3);

                if (IsSlip(middlePart))
                {
                    return true; // Slap in the form of 'A' followed by a slip, followed by 'C'
                }
            }

            return false; // Not a slap
        }

        public static bool IsSlop(string input)
        {
            int index = input.IndexOf("DF") + 1;
            if (index <= 0 || index >= input.Length - 1 || input[index] != 'G')
                return false;

            string slap = input.Substring(0, index + 1);
            string slip = input.Substring(index + 1);

            return (IsSlap(slap) || IsSlip(slap)) && IsSlip(slip);
        }

        public static void Main(string[] args)
        {
            string[] examples = {
                "DFG", "EFG", "DFFFFFG", "DFDFDFDFG", "DFEFFFFFG",
                "DFEFF", "EFAHG", "DEFG", "DG", "EFFFFDG",
                "AH", "ABAHC", "ABABAHCC", "ADFGC", "ADFFFFGC", "ABAEFGCC", "ADFDFGC",
                "ABC", "ABAH", "DFGC", "ABABAHC", "SLAP", "ADGCS",
                "AHDFG", "ADFGCDFFFFFG", "ABAEFGCCDFE", "AHDFG", "ADFGCDFFFFFG", "ABAEFGCCDFEFFFFFG", "AHDFG", "DFGAH", "ABABCC"
            };

            Console.WriteLine("A Slip is a character string that has the following properties:");
            Console.WriteLine("- Its first character is either a 'D' or an 'E'");
            Console.WriteLine("- The first character is followed by a string of one or more 'F's");
            Console.WriteLine("- The string of one or more 'F's is followed by either a Slip or a 'G'");
            Console.WriteLine("- The Slip or 'G' that follows the F's ends the Slip.");
            Console.WriteLine("- Nothing else is a Slip");
            Console.WriteLine("A Slap is a character string that has the following properties:");
            Console.WriteLine("- Its first character is an 'A'");
            Console.WriteLine("- If it is a two-character Slap, then its second and last character is an 'H'");
            Console.WriteLine("- If it is not a two-character Slap, then it is in one of these two forms:");
            Console.WriteLine("----- 'A' followed by 'B' followed by a Slap, followed by a 'C'");
            Console.WriteLine("----- 'A' followed by a Slip (see above) followed by a 'C'");
            Console.WriteLine("- Nothing else is a Slap");
            Console.WriteLine("A Slop is a character string that consists of a Slap followed by a Slip.");

            Console.WriteLine();

            foreach (string example in examples)
            {
                bool isSlip = IsSlip(example);
                bool isSlap = IsSlap(example);
                bool isSlop = IsSlop(example);

                if (isSlip)
                {
                    Console.WriteLine($"{example}: Slip");
                }
                else if (isSlap)
                {
                    Console.WriteLine($"{example}: Slap");
                }
                else if (isSlop)
                {
                    Console.WriteLine($"{example}: Slop");
                }
            }

            Console.ReadLine();
        }
    }
}

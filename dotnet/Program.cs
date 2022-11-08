// See https://aka.ms/new-console-template for more information




namespace EldenRingHyperArmourDump
{
    public class Program
    {
        static void Main(string[] args)
        {
            Util.DumpAnimData("all_toughnessParams.csv", @"../resources/1.07/c0000.anibnd.dcx");
        }
    }
    

}



// See https://aka.ms/new-console-template for more information




namespace EldenRingHyperArmourDump
{
    public class Program
    {
        static void Main(string[] args)
        {
            Util.DumpAnimData("animationToughnessParams.csv", @"../resources/1.07/");
            Util.DumpToughnessParamData("toughnessParams.csv", @"../resources/1.07/");
        }
    }
    

}



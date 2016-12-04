using CQRS.Commands;

namespace CQRSTest.Commands
{
    public class AddNumbersCommand : ICommand
    {
        public int A { get; set; }

        public int B { get; set; }

        public AddNumbersCommand(int a, int b)
        {
            A = a;
            B = b;
        }
    }
}
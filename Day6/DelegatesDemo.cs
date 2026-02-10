using System;

namespace DelegatesDemo
{
    // Custom EventArgs (not used currently, but kept correct)
    public class OnClickEventArgs : EventArgs
    {
        public string ButtonName { get; set; }
    }

    // Publisher
    public class Button
    {
        // Better practice: use EventHandler
        public event Action OnClick;

        // Raise event
        public void Click()
        {
            OnClick?.Invoke();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DelegatesDemoApp app = new DelegatesDemoApp();

            // Uncomment one by one to test
            // app.DelegateDemo();
            // app.AnonymousMethodDemo();
            // app.LambdaExpressionDemo();
            // app.HigherOrderFunctionDemo();

            Button button = new Button();

            // Subscribers
            button.OnClick += () => Console.WriteLine("Bell Rings!");
            button.OnClick += () => Console.WriteLine("Charge for Electricity!");
            button.OnClick += () => Console.WriteLine("Third!");
            button.OnClick += () => Console.WriteLine("Fourth!");

            // Raise Event
            button.Click();
        }
    }

    // Delegate declaration
    delegate int MathOperation(int a, int b);

    // Generic delegate
    delegate void GenericTwoParameterAction<TFirst, TSecond>(TFirst a, TSecond b);

    class DelegatesDemoApp
    {
        // ---------------- HIGHER ORDER FUNCTION ----------------
        public void HigherOrderFunctionDemo()
        {
            var result = CalculateArea(AreaOfRectangle);
            Console.WriteLine($"Area: {result}");
        }

        int CalculateArea(Func<int, int, int> areaFunction)
        {
            return areaFunction(5, 10);
        }

        int AreaOfRectangle(int length, int width)
        {
            return length * width;
        }

        // ---------------- LAMBDA ----------------
        public void LambdaExpressionDemo()
        {
            Func<int, int> square = x => x * x;

            var result = square(5);
            Console.WriteLine($"Result: {result}");
        }

        // ---------------- ANONYMOUS METHOD ----------------
        public void AnonymousMethodDemo()
        {
            MathOperation operation = delegate (int a, int b)
            {
                Console.WriteLine($"The sum of {a} and {b} is: {a + b}");
                return a + b;
            };

            operation(5, 3);
        }

        // ---------------- BASIC DELEGATES ----------------
        public void DelegateDemo()
        {
            Func<int, int, int> operation = Add;

            Action<string> action = PrintMessage;
            action("Hello from Action delegate!");

            Predicate<int> predicate = IsEven;
            Console.WriteLine($"Is 4 even? {predicate(4)}");

            operation += Subtract;
            operation += Multiply;

            int result = operation(10, 5); // only last method result returned
            Console.WriteLine($"Final result: {result}");
        }

        void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        // ---------------- METHODS ----------------
        public int Add(int a, int b)
        {
            Console.WriteLine($"Add: {a + b}");
            return a + b;
        }

        public int Subtract(int a, int b)
        {
            Console.WriteLine($"Subtract: {a - b}");
            return a - b;
        }

        public int Multiply(int a, int b)
        {
            Console.WriteLine($"Multiply: {a * b}");
            return a * b;
        }
    }
}

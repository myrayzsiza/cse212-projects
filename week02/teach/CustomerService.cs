/// <summary>
/// Maintain a Customer Service Queue. Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Test 1
        // Scenario: Create a queue with an invalid size (<= 0). Verify max size defaults to 10.
        // Expected Result: max_size should be 10.
        Console.WriteLine("Test 1");
        var cs1 = new CustomerService(0);
        Console.WriteLine(cs1); // Should show max_size=10
        // Defect(s) Found: None (constructor handles this requirement correctly).

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Add customer, then serve customer (FIFO order).
        // Expected Result: The customer that was added first should be displayed and served.
        Console.WriteLine("Test 2");
        var cs2 = new CustomerService(3);
        cs2.AddNewCustomer(); // Enter sample info when prompted
        cs2.ServeCustomer();  // Should display customer info and remove them
        // Defect(s) Found: ServeCustomer removed the item at index 0 BEFORE getting the customer,
        // causing index out of bounds or serving the wrong person.

        Console.WriteLine("=================");

        // Test 3
        // Scenario: Serve a customer when the queue is empty.
        // Expected Result: Error message displayed: "No customers in queue."
        Console.WriteLine("Test 3");
        var cs3 = new CustomerService(3);
        cs3.ServeCustomer();
        // Defect(s) Found: Attempting to serve an empty queue caused an ArgumentOutOfRangeException crash.

        Console.WriteLine("=================");

        // Test 4
        // Scenario: Add more customers than the maximum allowed size.
        // Expected Result: Error message displayed on the extra add attempt.
        Console.WriteLine("Test 4");
        var cs4 = new CustomerService(2);
        cs4.AddNewCustomer();
        cs4.AddNewCustomer();
        cs4.AddNewCustomer(); // This 3rd attempt should fail
        // Defect(s) Found: Queue size comparison used '>' instead of '>=', allowing size to exceed max.
        
        Console.WriteLine("=================");
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class. Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId}) : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information. Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        // FIX BUG 1: Changed '>' to '>='
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        // FIX BUG 2a: Check if queue is empty first
        if (_queue.Count == 0) {
            Console.WriteLine("No customers in queue.");
            return;
        }

        // FIX BUG 2b: Get customer info BEFORE removing from queue
        var customer = _queue[0];
        _queue.RemoveAt(0);
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}
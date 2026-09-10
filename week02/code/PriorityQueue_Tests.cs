using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue items with varying priorities ("A": 1, "B": 5, "C": 3, "D": 2) and dequeue all items.
    // Expected Result: "B", "C", "D", "A" (dequeued strictly from highest priority to lowest priority).
    // Defect(s) Found: The loop condition was (index < _queue.Count - 1), which skipped checking the last element in the queue. Dequeue also did not call _queue.RemoveAt(), leaving dequeued items in the queue.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);
        priorityQueue.Enqueue("D", 2);

        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("D", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with the same highest priority ("Item 1": 4, "Item 2": 2, "Item 3": 4).
    // Expected Result: "Item 1", "Item 3", "Item 2" (when priorities are equal, follow FIFO order).
    // Defect(s) Found: The comparison operator used was >= instead of >, which broke FIFO ordering by selecting the last occurrence of the highest priority item.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Item 1", 4);
        priorityQueue.Enqueue("Item 2", 2);
        priorityQueue.Enqueue("Item 3", 4);

        Assert.AreEqual("Item 1", priorityQueue.Dequeue());
        Assert.AreEqual("Item 3", priorityQueue.Dequeue());
        Assert.AreEqual("Item 2", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty PriorityQueue.
    // Expected Result: Throws InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None. The empty queue check worked as expected.
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (Exception e)
        {
            Assert.Fail($"Unexpected exception of type {e.GetType()} caught: {e.Message}");
        }
    }
}
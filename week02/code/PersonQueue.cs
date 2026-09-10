using System.Collections.Generic;

public class PersonQueue
{
    private readonly List<Person> _persons = new();

    public int Length => _persons.Count;

    public void Enqueue(Person person)
    {
        // Add to the back of the queue
        _persons.Add(person);
    }

    public Person Dequeue()
    {
        var person = _persons[0];
        _persons.RemoveAt(0);
        return person;
    }

    public bool IsEmpty()
    {
        return _persons.Count == 0;
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _persons)}]";
    }
}
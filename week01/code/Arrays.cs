public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Plan:
        // 1. Create a new double array named 'result' with a size equal to the 'length' parameter.
        // 2. Loop through indices from 0 up to 'length - 1'.
        // 3. In each iteration, calculate the multiple by multiplying 'number' by (i + 1).
        // 4. Store the calculated multiple at index 'i' in the 'result' array.
        // 5. Return the populated 'result' array.

        double[] result = new double[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Plan:
        // 1. Check if rotation is necessary (if amount equals data.Count or data is empty/null, return early).
        // 2. Calculate the starting index for the slice that needs to move to the front: (data.Count - amount).
        // 3. Extract the last 'amount' elements from the list using GetRange(startingIndex, amount).
        // 4. Remove those extracted elements from the end of the list using RemoveRange(startingIndex, amount).
        // 5. Insert the extracted elements at index 0 at the front of the list using InsertRange(0, extractedItems).

        if (data == null || data.Count == 0 || amount == data.Count)
        {
            return;
        }

        int startSliceIndex = data.Count - amount;
        List<int> tailSlice = data.GetRange(startSliceIndex, amount);

        data.RemoveRange(startSliceIndex, amount);
        data.InsertRange(0, tailSlice);
    }
}
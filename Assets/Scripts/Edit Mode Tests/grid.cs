using System.Collections.Generic;
using NUnit.Framework;

public class grid 
{
    [Test]
    public void by_size_4x4_has_correct_amount_of_faces()
    {
        GridGenerator gridGenerator = new GridGenerator(4, 1);
        gridGenerator.CreateProceduralGrid();

        int expectedValue = (4 * 4 * 4);
        
        Assert.AreEqual(expectedValue, gridGenerator.Faces.Count);
    }
    
    [Test]
    public void by_size_5x5_has_correct_amount_of_faces()
    {
        GridGenerator gridGenerator = new GridGenerator(5, 1);
        gridGenerator.CreateProceduralGrid();

        int expectedValue = (5 * 5 * 4);
        
        Assert.AreEqual(expectedValue, gridGenerator.Faces.Count);
    }
    
    [Test]
    public void by_size_6x6_has_correct_amount_of_faces()
    {
        GridGenerator gridGenerator = new GridGenerator(6, 1);
        gridGenerator.CreateProceduralGrid();

        int expectedValue = (6 * 6 * 4);
        
        Assert.AreEqual(expectedValue, gridGenerator.Faces.Count);
    }
    

}

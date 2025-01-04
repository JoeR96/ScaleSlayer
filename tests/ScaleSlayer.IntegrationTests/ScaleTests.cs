using FluentAssertions;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;
using LoopLearner.Web.Server.Dto;
using ScaleSlayer.IntegrationTests.Extensions;

namespace ScaleSlayer.IntegrationTests;
[TestFixture]
public class ScaleTests
{
    private static IEnumerable<TestCaseData> ScaleTestCases()
    {
        yield return new TestCaseData(
            "A",
            "PentatonicMinor",
            new Dictionary<int, HashSet<int>>()
            {
                { 1, new HashSet<int> { 5, 7, 8, 17, 19, 20 } }, 
                { 2, new HashSet<int> { 7, 8, 9, 10, 19, 20, 21, 22 } },  
                { 3, new HashSet<int> { 9, 10, 12, 13, 21, 22 } },   
                { 4, new HashSet<int> { 0, 1, 2, 3, 12, 13, 14, 15 } },   
                { 5, new HashSet<int> { 2, 3, 5, 14, 15, 17 } }    
            }
        );
        
        yield return new TestCaseData(
            "ASharp",
            "PentatonicMinor",
            new Dictionary<int, HashSet<int>>()
            {
                { 1, new HashSet<int> { 6, 8, 9, 18, 20, 21 } }, 
                { 2, new HashSet<int> { 8, 9, 10, 11, 20, 21, 22  } },  
                { 3, new HashSet<int> { 10, 11, 13, 14} },   
                { 4, new HashSet<int> { 1, 2, 3, 4, 13, 14, 15, 16 } },   
                { 5, new HashSet<int> { 3, 4, 6, 15, 16, 18 } }    
            }
        );
        
        yield return new TestCaseData(
            "B",
            "PentatonicMinor",
            new Dictionary<int, HashSet<int>>()
            {
                { 1, new HashSet<int> { 7, 9, 10, 19, 21, 22} }, 
                { 2, new HashSet<int> { 9, 10, 11, 12, 21, 22 } },  
                { 3, new HashSet<int> { 11, 12, 14, 15 } },   
                { 4, new HashSet<int> { 2, 3, 4, 5, 14, 15, 16, 17 } },   
                { 5, new HashSet<int> { 4, 5, 7, 16, 17, 19} }    
            }
        );
        
        yield return new TestCaseData(
            "C",
            "PentatonicMinor",
            new Dictionary<int, HashSet<int>>()
            {
                { 1, new HashSet<int> { 8, 10, 11, 20, 22 } }, 
                { 2, new HashSet<int> { 10, 11, 12 , 13 } },  
                { 3, new HashSet<int> { 0, 1, 3, 4, 12, 13, 15, 16 } },   
                { 4, new HashSet<int> { 3, 4, 5, 6, 15, 16, 17, 18 } },   
                { 5, new HashSet<int> { 5, 6, 8, 17, 18, 20 } }    
            }
        );
        
        yield return new TestCaseData(
            "CSharp",
            "PentatonicMinor",
            new Dictionary<int, HashSet<int>>()
            {
                { 1, new HashSet<int> { 9, 11, 12, 21 } }, 
                { 2, new HashSet<int> { 11, 12, 13, 14 } },  
                { 3, new HashSet<int> { 1, 2, 4, 5, 13, 14, 16, 17 } },   
                { 4, new HashSet<int> { 4, 5, 6, 7, 16, 17, 18, 19 } },   
                { 5, new HashSet<int> { 6, 7, 9, 18, 19, 21 } }    
            }
        );

        yield return new TestCaseData(
            "D",
            "PentatonicMinor",
            new Dictionary<int, HashSet<int>>()
            {
                { 1, new HashSet<int> { 10, 12, 13 } }, 
                { 2, new HashSet<int> { 0, 1, 2, 3, 12, 13, 14, 15 } },  
                { 3, new HashSet<int> { 2,3,5,6,14,15,17,18 } },   
                { 4, new HashSet<int> { 5, 6, 7, 8, 17, 18, 19, 20 } },   
                { 5, new HashSet<int> { 7, 8, 10, 19, 20, 22 } }    
            }
        );
        
        yield return new TestCaseData(
            "DSharp",
            "PentatonicMinor",
            new Dictionary<int, HashSet<int>>()
            {
                { 1, new HashSet<int> { 11, 13, 14 } }, 
                { 2, new HashSet<int> { 1, 2, 3, 4, 13, 14, 15, 16 } },  
                { 3, new HashSet<int> { 3, 4, 6, 7, 15, 16, 18, 19 } },   
                { 4, new HashSet<int> { 6, 7, 8, 9, 18, 19, 20, 21 } },   
                { 5, new HashSet<int> { 8, 9, 11, 20, 21 } }    
            }
        );
        
        yield return new TestCaseData(
            "E",
            "PentatonicMinor",
            new Dictionary<int, HashSet<int>>()
            {
                { 1, new HashSet<int> { 0, 2, 3, 12, 14, 15 } }, 
                { 2, new HashSet<int> { 2, 3, 4, 5, 13, 14, 15, 16, 17 } },  
                { 3, new HashSet<int> { 4, 5, 7, 8, 16, 17, 19, 20 } },   
                { 4, new HashSet<int> { 7, 8, 9, 10, 19, 20, 21, 22 } },   
                { 5, new HashSet<int> { 9, 10, 12, 21, 22 } }    
            }
        );
        
        yield return new TestCaseData(
            "F",
            "PentatonicMinor",
            new Dictionary<int, HashSet<int>>()
            {
                { 1, new HashSet<int> { 1, 3, 4, 13, 15, 16 } }, 
                { 2, new HashSet<int> { 3, 4, 5, 6, 15, 16, 17, 18 } },  
                { 3, new HashSet<int> { 5, 6, 8, 9, 17, 18, 20, 21 } },   
                { 4, new HashSet<int> { 8, 9, 10, 11, 20, 21, 22 } },   
                { 5, new HashSet<int> { 10, 11, 13 } }    
            }
        );
        
        yield return new TestCaseData(
            "FSharp",
            "PentatonicMinor",
            new Dictionary<int, HashSet<int>>()
            {
                { 1, new HashSet<int> { 2, 4, 5, 14, 16, 17 } }, 
                { 2, new HashSet<int> { 4, 5, 6, 7, 16, 17, 18, 19 } },  
                { 3, new HashSet<int> { 6, 7, 9, 10, 18, 19, 21, 22 } },   
                { 4, new HashSet<int> { 9, 10, 11, 12, 21, 22 } },   
                { 5, new HashSet<int> { 11, 12, 14 } }    
            }
        );
        
        yield return new TestCaseData(
            "G",
            "PentatonicMinor",
            new Dictionary<int, HashSet<int>>()
            {
                { 1, new HashSet<int> { 3, 5, 6, 15, 17, 18 } }, 
                { 2, new HashSet<int> { 5, 6, 7, 8, 17, 18, 19, 20 } },  
                { 3, new HashSet<int> { 7, 8, 10, 11, 19, 20, 22 } },   
                { 4, new HashSet<int> { 10, 11, 12, 13 } },   
                { 5, new HashSet<int> { 0, 1, 3, 12, 13, 15} }    
            }
        );
        
        yield return new TestCaseData(
            "GSharp",
            "PentatonicMinor",
            new Dictionary<int, HashSet<int>>()
            {
                { 1, new HashSet<int> { 4, 6, 7, 16, 18, 19 } }, 
                { 2, new HashSet<int> { 6, 7, 8, 9, 18, 19, 20, 21 } },  
                { 3, new HashSet<int> { 8, 9, 11, 12, 20, 21 } },   
                { 4, new HashSet<int> { 11, 12, 13, 14 } },   
                { 5, new HashSet<int> { 1, 2, 4, 13, 14, 16} }    
            }
        );
    }

    [Test, TestCaseSource(nameof(ScaleTestCases))]
    public async Task GetScale_ReturnsCorrectScaleData(
        string key, 
        string scaleType, 
        Dictionary<int, HashSet<int>> allowedFretNumbersPerBox)
    {
        var client = WebHostFixture.GetHttpClient();
        
        var requestUri = $"api/scales/{key}/{scaleType}";

        var scaleResponse = await client.GetAsync<ScaleNotesResponse>(requestUri);

        foreach (var box in allowedFretNumbersPerBox)
        {
            var boxPosition = box.Key;
            var allowedFretNumbers = box.Value;

            var invalidFretNumbers = scaleResponse.ScaleNotes[(ScaleBoxPosition)boxPosition]
                .Where(r => !allowedFretNumbers.Contains(r.position.fretNumber))  // Check fretNumber directly
                .ToList();

            Assert.IsEmpty(invalidFretNumbers, 
                $"Invalid fret numbers found in Box {boxPosition} for {key} {scaleType}. Fret numbers: {string.Join(", ", invalidFretNumbers.Select(r => r.position.fretNumber))}");
        }
    }
}

``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.16299.371 (1709/FallCreatorsUpdate/Redstone3)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
Frequency=2835938 Hz, Resolution=352.6170 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.1 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.2633.0
  Job-JWVBYN : .NET Framework 4.7.1 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.2633.0

Runtime=Clr  IsBaseline=True  

```
|            Method |  N |           Mean |         Error |        StdDev | Scaled | Rank |
|------------------ |--- |---------------:|--------------:|--------------:|-------:|-----:|
| **GaussEliminationE** |  **5** |      **26.687 us** |     **0.3780 us** |     **0.3536 us** |   **1.00** |    **1** |
|                   |    |                |               |               |        |      |
| GaussEliminationL |  5 |       8.979 us |     0.0603 us |     0.0564 us |   1.00 |    1 |
|                   |    |                |               |               |        |      |
| **GaussEliminationE** | **10** |     **257.511 us** |     **1.4237 us** |     **1.2620 us** |   **1.00** |    **1** |
|                   |    |                |               |               |        |      |
| GaussEliminationL | 10 |      50.084 us |     0.6517 us |     0.6096 us |   1.00 |    1 |
|                   |    |                |               |               |        |      |
| **GaussEliminationE** | **20** |   **3,201.414 us** |    **26.2935 us** |    **24.5949 us** |   **1.00** |    **1** |
|                   |    |                |               |               |        |      |
| GaussEliminationL | 20 |     339.555 us |     1.9210 us |     1.7969 us |   1.00 |    1 |
|                   |    |                |               |               |        |      |
| **GaussEliminationE** | **30** |  **15,250.548 us** |   **238.2572 us** |   **222.8659 us** |   **1.00** |    **1** |
|                   |    |                |               |               |        |      |
| GaussEliminationL | 30 |   1,126.846 us |    22.3955 us |    34.2003 us |   1.00 |    1 |
|                   |    |                |               |               |        |      |
| **GaussEliminationE** | **40** |  **46,898.952 us** |   **413.6372 us** |   **386.9165 us** |   **1.00** |    **1** |
|                   |    |                |               |               |        |      |
| GaussEliminationL | 40 |   2,576.141 us |    23.9491 us |    19.9986 us |   1.00 |    1 |
|                   |    |                |               |               |        |      |
| **GaussEliminationE** | **50** | **110,458.635 us** | **1,315.3590 us** | **1,230.3875 us** |   **1.00** |    **1** |
|                   |    |                |               |               |        |      |
| GaussEliminationL | 50 |   5,218.852 us |   103.9582 us |   152.3805 us |   1.00 |    1 |

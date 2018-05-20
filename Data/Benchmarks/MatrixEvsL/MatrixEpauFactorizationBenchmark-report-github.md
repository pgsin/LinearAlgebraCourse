``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.16299.371 (1709/FallCreatorsUpdate/Redstone3)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
Frequency=2835938 Hz, Resolution=352.6170 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.1 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.2633.0
  Job-JWVBYN : .NET Framework 4.7.1 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.2633.0

Runtime=Clr  IsBaseline=True  

```
|             Method |  N |           Mean |         Error |        StdDev | Scaled | Rank |
|------------------- |--- |---------------:|--------------:|--------------:|-------:|-----:|
| **EpauFactorizationE** |  **5** |      **19.070 us** |     **0.2698 us** |     **0.2253 us** |   **1.00** |    **1** |
|                    |    |                |               |               |        |      |
| EpauFactorizationL |  5 |       8.334 us |     0.1638 us |     0.3116 us |   1.00 |    1 |
|                    |    |                |               |               |        |      |
| **EpauFactorizationE** | **10** |     **217.772 us** |     **3.3234 us** |     **3.1087 us** |   **1.00** |    **1** |
|                    |    |                |               |               |        |      |
| EpauFactorizationL | 10 |      40.059 us |     0.7703 us |     0.9170 us |   1.00 |    1 |
|                    |    |                |               |               |        |      |
| **EpauFactorizationE** | **20** |   **3,032.556 us** |    **57.8927 us** |    **66.6693 us** |   **1.00** |    **1** |
|                    |    |                |               |               |        |      |
| EpauFactorizationL | 20 |     271.323 us |     4.5588 us |     4.2643 us |   1.00 |    1 |
|                    |    |                |               |               |        |      |
| **EpauFactorizationE** | **30** |  **14,424.280 us** |   **216.7718 us** |   **192.1626 us** |   **1.00** |    **1** |
|                    |    |                |               |               |        |      |
| EpauFactorizationL | 30 |     842.309 us |    13.0835 us |    11.5982 us |   1.00 |    1 |
|                    |    |                |               |               |        |      |
| **EpauFactorizationE** | **40** |  **44,867.089 us** |   **880.2002 us** |   **823.3397 us** |   **1.00** |    **1** |
|                    |    |                |               |               |        |      |
| EpauFactorizationL | 40 |   1,966.293 us |    41.9102 us |    39.2029 us |   1.00 |    1 |
|                    |    |                |               |               |        |      |
| **EpauFactorizationE** | **50** | **107,705.087 us** | **2,103.6983 us** | **2,066.1129 us** |   **1.00** |    **1** |
|                    |    |                |               |               |        |      |
| EpauFactorizationL | 50 |   3,712.596 us |    70.5923 us |    69.3311 us |   1.00 |    1 |

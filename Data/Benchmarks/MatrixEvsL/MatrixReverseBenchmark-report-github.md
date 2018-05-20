``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.16299.371 (1709/FallCreatorsUpdate/Redstone3)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
Frequency=2835938 Hz, Resolution=352.6170 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.1 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.2633.0
  Job-JWVBYN : .NET Framework 4.7.1 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.2633.0

Runtime=Clr  IsBaseline=True  

```
|   Method |  N |           Mean |       Error |        StdDev | Scaled | Rank |
|--------- |--- |---------------:|------------:|--------------:|-------:|-----:|
| **ReverseE** |  **5** |      **23.249 us** |   **0.1715 us** |     **0.1604 us** |   **1.00** |    **1** |
|          |    |                |             |               |        |      |
| ReverseL |  5 |       5.887 us |   0.0209 us |     0.0185 us |   1.00 |    1 |
|          |    |                |             |               |        |      |
| **ReverseE** | **10** |     **239.509 us** |   **2.8272 us** |     **2.6446 us** |   **1.00** |    **1** |
|          |    |                |             |               |        |      |
| ReverseL | 10 |      30.142 us |   0.2822 us |     0.2640 us |   1.00 |    1 |
|          |    |                |             |               |        |      |
| **ReverseE** | **20** |   **3,066.745 us** |  **23.0489 us** |    **21.5599 us** |   **1.00** |    **1** |
|          |    |                |             |               |        |      |
| ReverseL | 20 |     200.954 us |   1.1413 us |     1.0676 us |   1.00 |    1 |
|          |    |                |             |               |        |      |
| **ReverseE** | **30** |  **14,617.600 us** | **127.3373 us** |   **119.1113 us** |   **1.00** |    **1** |
|          |    |                |             |               |        |      |
| ReverseL | 30 |     643.100 us |   4.1965 us |     3.9254 us |   1.00 |    1 |
|          |    |                |             |               |        |      |
| **ReverseE** | **40** |  **45,164.157 us** | **873.5915 us** | **1,039.9486 us** |   **1.00** |    **1** |
|          |    |                |             |               |        |      |
| ReverseL | 40 |   1,516.950 us |   9.1970 us |     8.1529 us |   1.00 |    1 |
|          |    |                |             |               |        |      |
| **ReverseE** | **50** | **107,222.172 us** | **512.5845 us** |   **454.3928 us** |   **1.00** |    **1** |
|          |    |                |             |               |        |      |
| ReverseL | 50 |   2,870.245 us |  17.3565 us |    16.2353 us |   1.00 |    1 |

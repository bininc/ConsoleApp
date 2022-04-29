测试4MB大小的字节数组的比较<br/>
===
#### 测试环境：
    .NET Framework 4.0、 4.5.2、 4.6.2、 4.7.2、 4.8
    .NET Core 2.1、 3.1
    .NET 5.0、 6.0
><font color=#2f4cd5>.NET Framework 4.5 之后支持Vector SIMD加速</br>
.NET Core 3.1及之后支持调用CPU指令集进行加速</font>

![image](https://user-images.githubusercontent.com/3025159/165794159-919db952-4009-43dc-98df-903715e04397.png)
![image](https://user-images.githubusercontent.com/3025159/165794319-d7ee1044-9f7d-434e-aeee-f3cb3ba10db0.png)
![image](https://user-images.githubusercontent.com/3025159/165794381-a39331e6-cc29-4aa1-b1b4-e613409d34ce.png)
![image](https://user-images.githubusercontent.com/3025159/165794084-e9bad752-e1b2-4460-95ac-eacc7102740f.png)
![image](https://user-images.githubusercontent.com/3025159/165793993-64a771e3-eb29-49e8-b784-307ae38d2287.png)
![image](https://user-images.githubusercontent.com/3025159/165793640-fa767900-a468-49a9-a635-9feeb11fd29b.png)
![image](https://user-images.githubusercontent.com/3025159/165794263-54f87837-6f15-4a34-a5d5-b9828679cbac.png)

### 结论：
> 兼顾性能和兼容性可采用Vector方式 (.NET Framework 4.0不支持可采用Bit64方法充分利用64位处理器)

> .NET Core3.1及以上可采用Avx2指令集一次处理256位

> 从比较结果可以看出.NET 6.0性能提升明显并且对SequenceEquals方法底层也做了加速优化，强烈建议升级到.NET 6.0
## TDBI HOLD BIB RULE

#### Linefail HOLD

- 连续10个LOT发生3次及以上LINEFAIL；（8F Type板连续10个LOT发生2次及以上LINEFAIL；）


- 判定Linefail(同时满足):
  1. 该Row或该Column的上板数量不小于5颗(可不连续); 
  2. 该Row或该Column的连续fail 3颗及以上
  3. 该Row或该Columnfail率大于50% 



#### 全板/半板 HOLD

- 行Linefail条数 >= (( 行总条数 / 2.0) - 2.0)

  *Linefail条数规则与上述LinefailHold判定一致，行总数计算不论是否有IC*



#### LowYield HOLD - Sigma Rule

- 连续10个LOT发生3次及以上LowYield；（8F Type板连续10个LOT发生2次及以上LowYield；）

- 判定LowYield计算公式：
  $$
  LotSigma 计算公式：
  [
  \text{LotSigma} = \sqrt{\frac{\sum_{n=0}^{BIBQty} (BoardYield - LotYield)^2}{BIBQty}}
  ]
  $$
  
  $$
  Single Low Yield 条件：
  [
  \text{Single Low Yield: } BoardYield > LotYield - \Sigma Rate \times \sqrt{\frac{\sum_{n=0}^{BIBQty} (BoardYield - LotYield)^2}{BIBQty}}
  ]
  $$
  *Sigma Rate = 2*



#### LowYield HOLD

- 板良率低于60% 且 总IC数大于112颗；
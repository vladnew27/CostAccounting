This program in C#  allows a user to perform 4 calculations based on the sale of any number of shares of stock at any price utilizing one of the accounting strategies.

The calculations are as follows:
1. The remaining number of shares after the sale 
2. The cost basis per share of the sold shares
3. The cost basis per share of the remaining shares after the sale
4. The total profit or loss of the sale

#### As for 08.08.2024 implemented only the FIFO method: 
FIFO (first in / first out) – This strategy sells shares from the first lot that you purchased and then moves on to shares from the subsequent lot after each lots’ shares have been disposed of.

For editing purchase lots find out `bin\Debug\net8.0\Data\stock_data.json` or `bin\Release\net8.0\Data\stock_data.json` and edit the json objects.
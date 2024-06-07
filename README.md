# CSharpSummaryReport
This program analyzes sales data a CSV file (CodeChallenge.csv), and produces a summary report. The CSV file contains the following columns:

**Date:** The date of the sale. (format: YYYY-MM-DD)
**SalesPerson:** The name of the salesperson.
**Region:** The sales region.
**Product:** The product sold.
**Quantity:** The quantity sold.
**Price:** The price per unit of the product.

This C# program performs the following tasks:
 - Read the CSV file and load the data into an appropriate data structure.
 - Calculate the total sales amount for each salesperson. (Total Sales = Quantity * Price)
 - Find the top-selling product in each region.
 - Compute the average sales per day for each region.

This C# program then generate a summary report (as text seen in the VSCode Debug Console) that includes:
 - Total sales per salesperson.
 - Top-selling product in each region.
 - Average sales per day in each region.

For the code, please go to: 'SummaryReport/SummaryReport/Program.cs'.

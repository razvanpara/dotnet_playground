# **USAGE**

## You can either run it as it is or you can pass command line parameters:
### There are 5 parameters that are supported, they must pe provided in the exact order
### Parameters are:
1. The rule to be displayed, must fit into a byte (8 bits long), defaults to 110
2. The board size as an int32, defaults to the size of the console window
3. The number of iterations as an int32, defaults to 1 000 000
4. The number of fps as an int32 (max 1000 for 1ms frame time), defaults to 24
5. The bool value that specifies if the starting board should be randomized, defaults to false 

## Example:
```
CellularAutomationRule.exe 110 32 100 24 true
```
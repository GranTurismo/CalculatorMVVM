<!--README FILE-->
<!--HOW TO ADD NEW OPERATION IN THIS MVVM CALCULATOR PROJECT-->

To do that, for first you need to add new button in the UI.
1. Go to MainWindow.xaml file and add new button element. Set position in the Grid as you like, set style and go to second step
2. Open CalculatorViewModel and add new Command and implement Execute and CanExecute methods. It should be implemented in the CalculatorModel.
3. Open Operation.cs file, add your operation enum.
4. Open CalculatorModel and find _equalButtonDelegateDictionary initialization in the constructor. Write a lambda for your operation
5. Link your button to that command that you created in the second step 
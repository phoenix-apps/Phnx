# Phnx.Console

This library contains tools to help you read and write to and from the console, including progress bars and getting specific types

# Quick Examples

For a full list of all things possible with Phnx.Console, please check the [API reference guide](https://phoenix-apps.github.io/Phnx-Wiki/api/Phnx.Console.html)

## ConsoleHelper

#### Get&lt;T&gt;
```cs
ConsoleHelper console = new ConsoleHelper();

DateTime dateOfBirth = console.Get<DateTime>(DateTime.Parse, "Please enter your date of birth: ");

// Output example: Your date of birth is 01/06/1972
console.WriteLine("Your date of birth is " + dateOfBirth.ToString("d"));
```

#### GetSelection
```cs
ConsoleHelper console = new ConsoleHelper();

List<string> options = new List<string>
{
    "First option",
    "Second option",
    "Third option"
};

/* Output
Select an option
1: First option
2: Second option
3: Third option
*/
int selected = console.GetSelection(options, "Select an option");

// Output (example): You chose "Second option"
console.WriteLine("You chose \"" + options[selected] + "\"");
```

#### Progress Bar (Fail)

The progress bar updates on the console from a separate thread, leaving you free to continue to run your own calculations in the main thread, whilst the progress bar continues to update itself

```cs
ConsoleHelper console = new ConsoleHelper();

// Output (in yellow): [----------] 0%
using (ConsoleProgress progressBar = Console.ProgressBar(100))
{
    // Simulate failing partway
    while (progressBar.Progress < 73)
    {
        progressBar.Progress++;
        Thread.Sleep(25);
    }

    throw new Exception("An error occurred");
}
// Output (in red): [#######---] 73%
```

#### Progress Bar (Success)
```cs
ConsoleHelper console = new ConsoleHelper();

// Output (in yellow): [----------] 0%
using (ConsoleProgress progressBar = Console.ProgressBar(100))
{
    // Simulate progress to completion
    while (progressBar.Progress < 100)
    {
        progressBar.Progress++;
        Thread.Sleep(25);
    }
}
// Output (in green): [##########] 100%
```
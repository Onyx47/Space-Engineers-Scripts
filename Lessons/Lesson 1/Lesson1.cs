public Program()
{
    // Sets up the automatic update of the script:
    // UpdateFrequency.Once: Runs the script once after recompilation
    // UpdateFrequency.Update10:  Runs the script every 10 game cycles  (~0.16 seconds)
    // UpdateFrequency.Update100: Runs the script every 100 game cycles (~1.66. seconds)
    Runtime.UpdateFrequency = UpdateFrequency.Once | UpdateFrequency.Update10 | UpdateFrequency.Update100;
}

public void Save()
{
    
}

public void Main(string argument, UpdateType updateType)
{
    // Gets the interior light blocks with the name "Light Terminal"
    // and runs when the "Run" button is clicked in Programmable block's terminal
    if ((updateType & UpdateType.Terminal) != 0)
    {
        var terminalLight = 
            (IMyInteriorLight) // Casts the block from generic IMyTerminalBlock to IMyInteriorLight
            GridTerminalSystem.GetBlockWithName("Light Terminal"); // Gets the block with specified name

        // Enabled property stores whether the block is on or not
        // If block is on, value of Enabled is "true"
        // If block is off, value of Enabled is "false"
        terminalLight.Enabled 
            = !terminalLight.Enabled;   // By using ! in front of current value of Enabled propery
                                        // we flip it to its opposite state.
                                        // If the block is on this will be true, !true = false
    }
    
    // Gets the interior light blocks with the name "Light Trigger"
    // and runs when the Programmable block is activated by an action on another block
    // such as a timer or a sensor
    if ((updateType & UpdateType.Trigger) != 0)
    {
        var triggerLight = (IMyInteriorLight)GridTerminalSystem.GetBlockWithName("Light Trigger");
        triggerLight.Enabled = !triggerLight.Enabled;
    }
    
    // Gets the interior light blocks with the name "Light Once"
    // and runs once after the script has been recompiled
    if ((updateType & UpdateType.Once) != 0)
    {
        var onceLight = (IMyInteriorLight)GridTerminalSystem.GetBlockWithName("Light Once");
        onceLight.Enabled = !onceLight.Enabled;
    }
    
    // Gets the interior light blocks with the name "Light Once"
    // and runs every 10 cycles
    if ((updateType & UpdateType.Update10) != 0)
    {
        var update10Light = (IMyInteriorLight)GridTerminalSystem.GetBlockWithName("Light Update10");
        update10Light.Enabled = !update10Light.Enabled;
    }
    
    // Gets the interior light blocks with the name "Light Once"
    // and runs every 100 cycles
    if ((updateType & UpdateType.Update100) != 0)
    {
        var update100Light = (IMyInteriorLight)GridTerminalSystem.GetBlockWithName("Light Update100");
        update100Light.Enabled = !update100Light.Enabled;

        // We get a battery block named "Battery"
        var battery = (IMyBatteryBlock)GridTerminalSystem.GetBlockWithName("Battery");

        // Detailed info contains the text shown in lower right of block's terminal interface
        var info = battery.DetailedInfo;

        // string.Split() method splits a string into an array of strings (similar to a list)
        // \n denotes a new line.
        // Here, we're splitting the detailed info into distinct lines
        var lines = info.Split('\n');

        // The foreach loop will go through the entire array automatically
        // "line" variable will hold an individual line on every pass of the loop
        foreach (var line in lines)
        {
            // We check if the line contains the text "Fully depleted"
            if (line.Contains("Fully depleted"))
            {
                // Lines are in format <Key>: <Value>
                // Splitting on ':' character we split them into an array with 2 members:
                // For example: ["Fully depleted in", "5 minutes"]
                var keyValue = line.Split(':');

                // Arrays are 0-indexed.
                // In this example the string "Fully depleted in" is at index 0
                // while the string "5 minutes" is at index 1
                // To get a single value from the array we use square brackets with the index
                var depletedIn = keyValue[1].Trim();    // Trim method removes any spaces 
                                                        // from beginning and end of a string

                // This will print out the text "Your battery will die in 5 minutes"
                // Into the detailed info section of your programmable block
                Echo("Your battery will die in " + depletedIn);
            }
        }
    }
}
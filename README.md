# UnityEditorAutoSave

## An Editor Auto-Save System For The Unity Game Engine With Editable Parameters.
Enough said. Functionality included:

![image](https://github.com/TheToolmansCoffee/UnityEditorAutoSave/assets/93699568/fcbceedb-ca2c-4846-a51e-bf76af9d8731)

# Parameters Explained

## Enable Auto-Save 
### Disables/Enables the autosave, for some reason.

## Save Interval (s)
### The time between auto-saves, in seconds.

## Enable Status Messages
### Enables status messages in the console (Debug.Log) (I.e. notes when currently auto-saving, when auto-save has finished)

## Only Remind On Save
### If Enable Status Messages is enabled, skips the currently saving status message and only notes when autosave is finished.

## Saving-Message
### The message displayed in the console (Debug.Log) when auto-saving begins

## Saved Message
### The message displayed in the console (Debug.Log) when auto-saving has finished.

## Other Notes
### Performance is great, especially in smaller scenes. The Interval should never be set below one, as that causes a massive performance impact.

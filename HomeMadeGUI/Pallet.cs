using Raylib_cs;

namespace HomeMadeGUI;

public class Pallet
{
    // Core Colors
    public static Color PrimaryColor;
    public static Color SecondaryColor;
    public static Color AccentColor; // For special highlights or callouts

    // Text Colors
    public static Color PrimaryTextColor;
    public static Color SecondaryTextColor; // Subtle or less important text
    public static Color DisabledTextColor;
    public static Color ErrorTextColor;
    public static Color SuccessTextColor;

    // Background Colors
    public static Color BackgroundColor; // Main app background
    public static Color CardBackgroundColor; // For card components
    public static Color ModalBackgroundColor; // For modals and overlays
    public static Color HighlightBackgroundColor; // For selected or focused items

    // Button Colors
    public static Color ButtonPrimaryColor; // Default button background
    public static Color ButtonSecondaryColor; // Secondary button background
    public static Color ButtonTextColor;
    public static Color ButtonDisabledColor;
    public static Color ButtonHoverColor;
    public static Color ButtonActiveColor;

    // Border Colors
    public static Color BorderColor; // Default border color
    public static Color FocusBorderColor; // When an element gains focus
    public static Color ErrorBorderColor; // For error states

    // Input Field Colors
    public static Color InputBackgroundColor; // Background of text inputs
    public static Color InputTextColor;
    public static Color InputPlaceholderColor;
    public static Color InputBorderColor;
    public static Color InputFocusBorderColor;

    // Notification Colors
    public static Color SuccessColor; // Success messages or indicators
    public static Color ErrorColor; // Error messages or indicators
    public static Color WarningColor; // Warnings
    public static Color InfoColor; // Informational messages

    // Status Colors
    public static Color OnlineStatusColor;
    public static Color OfflineStatusColor;
    public static Color BusyStatusColor;
    public static Color AwayStatusColor;

    // Graph/Chart Colors
    public static Color ChartPrimaryColor;
    public static Color ChartSecondaryColor;
    public static Color ChartTertiaryColor;
    public static Color ChartBackgroundColor;

    // Miscellaneous Colors
    public static Color ShadowColor; // Shadow effects
    public static Color DividerColor; // Line separators
    public static Color TooltipBackgroundColor; // For tooltips
    public static Color TooltipTextColor;

    // App-Specific Custom Colors (example)
    public static Color NotificationBadgeColor;
    public static Color ProgressBarColor;

    public static void InitializeColors()
    {
        PrimaryColor = new Color(54, 69, 79, 255); 
        SecondaryColor = new (83, 104, 120, 200); 
        AccentColor = new (255, 255, 193, 7); //work
    
        PrimaryTextColor = Color.Black;
        SecondaryTextColor = Color.DarkGray;
        DisabledTextColor = Color.Gray;
        ErrorTextColor = Color.Red;
        SuccessTextColor = Color.Green;

        BackgroundColor = new(112,128,144,255);
        CardBackgroundColor = new(255, 248, 249, 250);
        ModalBackgroundColor = new(240, 240, 240,255);
        HighlightBackgroundColor = new(255, 232, 245, 255);

        ButtonPrimaryColor = new Color(77,117,154,255);
        ButtonSecondaryColor = new Color(111, 162, 208, 255);
        ButtonTextColor = Color.Black;
        ButtonDisabledColor = Color.LightGray;
        ButtonHoverColor = new Color(7, 55, 99, 255);
        ButtonActiveColor = new(5, 44, 79, 200);

        BorderColor = Color.LightGray;
        FocusBorderColor = PrimaryColor;
        ErrorBorderColor = ErrorTextColor;

        InputBackgroundColor = BackgroundColor;
        InputTextColor = PrimaryTextColor;
        InputPlaceholderColor = SecondaryTextColor;
        InputBorderColor = BorderColor;
        InputFocusBorderColor = FocusBorderColor;

        SuccessColor = SuccessTextColor;
        ErrorColor = ErrorTextColor;
        WarningColor = Color.Orange;
        InfoColor = Color.Blue;

        OnlineStatusColor = Color.Green;
        OfflineStatusColor = Color.Gray;
        BusyStatusColor = Color.Red;
        AwayStatusColor = Color.Orange;

        ChartPrimaryColor = PrimaryColor;
        ChartSecondaryColor = SecondaryColor;
        ChartTertiaryColor = AccentColor;
        ChartBackgroundColor = BackgroundColor;

        ShadowColor = new(64, 0, 0, 0); // Semi-transparent black
        DividerColor = BorderColor;
        TooltipBackgroundColor = new(255, 50, 50, 50);
        TooltipTextColor = Color.White;

        NotificationBadgeColor = ErrorTextColor;
        ProgressBarColor = PrimaryColor;
    }

}
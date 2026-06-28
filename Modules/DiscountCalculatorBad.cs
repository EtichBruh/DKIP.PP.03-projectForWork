namespace ReviewSamples.Modules;

public class DiscountCalculatorBad {
    public double Calc(double x, string t) {
        double d = 0;

        if (t == "vip") {
            d = x * 0.2;
        }
        else if (t == "regular") {
            d = x * 0.1;
        }
        else if (t == "new") {
            d = x * 0.05;
        }

        if (x > 1000) {
            d = d + 50;
        }

        return x - d;
    }
}

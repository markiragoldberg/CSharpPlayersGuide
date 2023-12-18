Card[] deck = Card.CreateDeck();

foreach (Card card in deck)
{
    Console.WriteLine($"The {card}");
}

// ---------------------------

class Card
{
    public CardColor Color { get; }
    public CardRank Rank { get; }
    public bool IsNumber => Rank <= CardRank.Ten;
    public bool IsSymbol => !IsNumber;

    public Card(CardColor color, CardRank rank) { Color = color; Rank = rank; }
    public static Card[] CreateDeck()
    {
        Card[] deck = new Card[4 * 14];
        for (int color = 0; color < 4; color++)
        {
            for (int rank = 0; rank < 14; rank++)
            {
                deck[color * 14 + rank] = new((CardColor)color, (CardRank)rank);
            }
        }
        return deck;
    }
    public override string ToString() => $"{Color} {Rank}";
}

enum CardColor { Red, Green, Blue, Yellow }
enum CardRank { 
    One, Two, Three, Four, Five, 
    Six, Seven, Eight, Nine, Ten, 
    Dollar, Percent, Caret, Ampersand }


// We used an enum here for the card color, instead of an actual color,
// because the color here is symbolic. It's a form of identifier for a
// subset of the cards in the deck. You are only supposed to have four 
// card subsets with these four identifiers, not any color you want.
// The card color here is/would be associated with a real color for
// display purposes, but not for internally identifying the cards.

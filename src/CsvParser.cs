#region ENBREA.CSV - Copyright (C) 2022 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2022 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;
using System.Text;
using System.Threading.Tasks;

namespace Enbrea.Csv
{
    /// <summary>
    /// A CSV parser which parses values from an abstract stream of characters.
    /// </summary>
    public class CsvParser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvParser"/> class.
        /// </summary>
        public CsvParser(Func<string, Exception> throwException)
        {
            Token = new StringBuilder();
            ThrowException = throwException;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvParser"/> class.
        /// </summary>
        /// <param name="separator">Specifies the charactor for seperating values</param>
        public CsvParser(char separator, Func<string, Exception> throwException)
            : this(throwException)
        {
            Configuration.Separator = separator;
        }

        /// <summary>
        /// Possible categories of a character
        /// </summary>
        public enum CharCategory { IsOrdinary, IsWhitespace, IsQuote, IsSeparator, IsComment, IsEndOfLine, IsEndOfFile }

        /// <summary>
        /// Possible states for the tokenizer
        /// </summary>
        public enum TokenizerState { IsSeekingStart, IsInPlain, IsInQuoted, IsAfterEndQuote, IsSkippingTail, IsInComment, IsAfterComment, IsEndOfLine }

        /// <summary>
        /// Possible workflow commands for the tokenizer
        /// </summary>
        public enum TokenizerWorkflow { IgnoreToken, ConsumeToken, Continue }

        /// <summary>
        /// Configuration parameter
        /// </summary>
        public CsvConfiguration Configuration { get; set; } = new CsvConfiguration();

        /// <summary>
        /// Current tokenizer state
        /// </summary>
        public TokenizerState State { get; private set; }

        /// <summary>
        /// Creates an exception which describes an error that occur during CSV parsing. 
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <returns>The newly craeted Exception instance</returns>
        /// <remarks>This method must be overwritten in derived classes.</remarks>
        public Func<string, Exception> ThrowException { get; }

        /// <summary>
        /// Current token
        /// </summary>
        public StringBuilder Token { get; }

        /// <summary>
        /// Parses CSV source for next token.
        /// </summary>
        /// <param name="nextChar">A method which gives back the next char 
        /// from a CSV source</param>
        /// <returns>true if token could be read; otherwise false.</returns>
        public bool NextToken(Func<char> nextChar)
        {
            Token.Clear();
            while (true)
            {
                switch (Parse(nextChar()))
                {
                    case TokenizerWorkflow.IgnoreToken:
                        return false;
                    case TokenizerWorkflow.ConsumeToken:
                        return true;
                }
            }
        }

        /// <summary>
        /// Parses CSV source for next token.
        /// </summary>
        /// <param name="nextCharAsync">An async method which gives back the next char 
        /// from a CSV source</param>
        /// <returns>A task that represents the asynchronous operation. The value of the 
        /// TResult parameter is true if token could be read; otherwise false.</returns>
        public async ValueTask<bool> NextTokenAsync(Func<ValueTask<char>> nextCharAsync)
        {
            Token.Clear();
            while (true)
            {
                switch (Parse(await nextCharAsync()))
                {
                    case TokenizerWorkflow.IgnoreToken:
                        return false;
                    case TokenizerWorkflow.ConsumeToken:
                        return true;
                }
            }
        }

        /// <summary>
        /// Resets the tokenizer state
        /// </summary>
        public void ResetState()
        {
            State = TokenizerState.IsSeekingStart;
        }

        /// <summary>
        /// Categorise a character.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <returns>Category of the character.</returns>
        private CharCategory Categorise(char c)
        {
            if (c == '\0')
            {
                return CharCategory.IsEndOfFile;
            }
            else if ((c == '\n') || (c == '\r'))
            {
                return CharCategory.IsEndOfLine;
            }
            else if (!(Configuration.IgnoreQuotes) && (c == Configuration.Quote))
            {
                return CharCategory.IsQuote;
            }
            else if (c == Configuration.Separator)
            {
                return CharCategory.IsSeparator;
            }
            else if ((Configuration.AllowComments) && (c == Configuration.Comment))
            {
                return CharCategory.IsComment;
            }
            else if (char.IsWhiteSpace(c))
            {
                return CharCategory.IsWhitespace;
            }
            else
            {
                return CharCategory.IsOrdinary;
            }
        }

        /// <summary>
        /// Parses given character and updates internal state.
        /// </summary>
        /// <param name="c">The character</param>
        /// <returns>Next workflow</returns>
        private TokenizerWorkflow Parse(char c)
        {
            switch (State)
            {
                case TokenizerState.IsSeekingStart:
                    {
                        switch (Categorise(c))
                        {
                            case CharCategory.IsOrdinary:
                            case CharCategory.IsWhitespace:
                                State = TokenizerState.IsInPlain;
                                Token.Append(c);
                                return TokenizerWorkflow.Continue;
                            case CharCategory.IsQuote:
                                State = TokenizerState.IsInQuoted;
                                return TokenizerWorkflow.Continue;
                            case CharCategory.IsSeparator:
                                return TokenizerWorkflow.ConsumeToken;
                            case CharCategory.IsComment:
                                State = TokenizerState.IsInComment;
                                return TokenizerWorkflow.Continue;
                            case CharCategory.IsEndOfFile:
                            case CharCategory.IsEndOfLine:
                                State = TokenizerState.IsEndOfLine;
                                return TokenizerWorkflow.ConsumeToken;
                        }
                        return TokenizerWorkflow.Continue;
                    }

                case TokenizerState.IsEndOfLine:
                    {
                        switch (Categorise(c))
                        {
                            case CharCategory.IsOrdinary:
                            case CharCategory.IsWhitespace:
                                State = TokenizerState.IsInPlain;
                                Token.Append(c);
                                return TokenizerWorkflow.Continue;
                            case CharCategory.IsQuote:
                                State = TokenizerState.IsInQuoted;
                                return TokenizerWorkflow.Continue;
                            case CharCategory.IsSeparator:
                                return TokenizerWorkflow.ConsumeToken;
                            case CharCategory.IsComment:
                                State = TokenizerState.IsInComment;
                                return TokenizerWorkflow.Continue;
                            case CharCategory.IsEndOfFile:
                                return TokenizerWorkflow.IgnoreToken;
                        }
                        return TokenizerWorkflow.Continue;
                    }

                case TokenizerState.IsInComment:
                    {
                        switch (Categorise(c))
                        {
                            case CharCategory.IsEndOfLine:
                                State = TokenizerState.IsAfterComment;
                                return TokenizerWorkflow.Continue;
                            case CharCategory.IsEndOfFile:
                                State = TokenizerState.IsEndOfLine;
                                return TokenizerWorkflow.IgnoreToken;
                        }
                        return TokenizerWorkflow.Continue;
                    }

                case TokenizerState.IsAfterComment:
                    {
                        switch (Categorise(c))
                        {
                            case CharCategory.IsOrdinary:
                                State = TokenizerState.IsInPlain;
                                Token.Append(c);
                                return TokenizerWorkflow.Continue;
                            case CharCategory.IsWhitespace:
                                State = TokenizerState.IsSeekingStart;
                                return TokenizerWorkflow.Continue;
                            case CharCategory.IsQuote:
                                State = TokenizerState.IsInQuoted;
                                return TokenizerWorkflow.Continue;
                            case CharCategory.IsSeparator:
                                State = TokenizerState.IsSeekingStart;
                                return TokenizerWorkflow.ConsumeToken;
                            case CharCategory.IsComment:
                                State = TokenizerState.IsInComment;
                                return TokenizerWorkflow.Continue;
                            case CharCategory.IsEndOfLine:
                                return TokenizerWorkflow.Continue;
                            case CharCategory.IsEndOfFile:
                                State = TokenizerState.IsEndOfLine;
                                return TokenizerWorkflow.IgnoreToken;
                        }
                        return TokenizerWorkflow.Continue;
                    }

                case TokenizerState.IsInPlain:
                    {
                        switch (Categorise(c))
                        {
                            case CharCategory.IsOrdinary:
                            case CharCategory.IsComment:
                            case CharCategory.IsWhitespace:
                                Token.Append(c);
                                return TokenizerWorkflow.Continue;
                            case CharCategory.IsQuote:
                                throw ThrowException($"CSV quote is missing at the begining.");
                            case CharCategory.IsSeparator:
                                State = TokenizerState.IsSeekingStart;
                                return TokenizerWorkflow.ConsumeToken;
                            case CharCategory.IsEndOfLine:
                            case CharCategory.IsEndOfFile:
                                State = TokenizerState.IsEndOfLine;
                                return TokenizerWorkflow.ConsumeToken;
                        }
                        return TokenizerWorkflow.Continue;
                    }

                case TokenizerState.IsInQuoted:
                    {
                        switch (Categorise(c))
                        {
                            case CharCategory.IsOrdinary:
                            case CharCategory.IsSeparator:
                            case CharCategory.IsComment:
                            case CharCategory.IsEndOfLine:
                                Token.Append(c);
                                return TokenizerWorkflow.Continue;
                            case CharCategory.IsWhitespace:
                                Token.Append(" ");
                                return TokenizerWorkflow.Continue;
                            case CharCategory.IsQuote:
                                State = TokenizerState.IsAfterEndQuote;
                                return TokenizerWorkflow.Continue;
                            case CharCategory.IsEndOfFile:
                                throw ThrowException($"CSV quote is missing at the end.");
                        }
                        return TokenizerWorkflow.Continue;
                    }

                case TokenizerState.IsAfterEndQuote:
                    {
                        switch (Categorise(c))
                        {
                            case CharCategory.IsOrdinary:
                            case CharCategory.IsComment:
                                throw ThrowException($"CSV separator is missing after quote.");
                            case CharCategory.IsWhitespace:
                                State = TokenizerState.IsSkippingTail;
                                return TokenizerWorkflow.Continue;
                            case CharCategory.IsQuote:
                                State = TokenizerState.IsInQuoted;
                                Token.Append(c);
                                return TokenizerWorkflow.Continue;
                            case CharCategory.IsSeparator:
                                State = TokenizerState.IsSeekingStart;
                                return TokenizerWorkflow.ConsumeToken;
                            case CharCategory.IsEndOfLine:
                            case CharCategory.IsEndOfFile:
                                State = TokenizerState.IsEndOfLine;
                                return TokenizerWorkflow.ConsumeToken;
                        }
                        return TokenizerWorkflow.Continue;
                    }

                case TokenizerState.IsSkippingTail:
                    {
                        switch (Categorise(c))
                        {
                            case CharCategory.IsOrdinary:
                            case CharCategory.IsComment:
                            case CharCategory.IsQuote:
                                throw ThrowException($"CSV separator is missing.");
                            case CharCategory.IsSeparator:
                                State = TokenizerState.IsSeekingStart;
                                return TokenizerWorkflow.Continue;
                            case CharCategory.IsEndOfLine:
                            case CharCategory.IsEndOfFile:
                                State = TokenizerState.IsEndOfLine;
                                return TokenizerWorkflow.ConsumeToken;
                        }
                        return TokenizerWorkflow.Continue;
                    }
            }
            return TokenizerWorkflow.Continue;
        }
    }
}
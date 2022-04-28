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
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Enbrea.Csv
{
    /// <summary>
    /// A CSV parser which parses values from an abstract stream of characters.
    /// </summary>
    /// <remarks>
    /// We are using Knuth's Multiplicative Hash for string hashing. Implementation is 
    /// borrowed from https://stackoverflow.com/a/9545731/18161314
    /// </remarks>
    public class CsvParser
    {
        private readonly StringBuilder _token;
        private readonly Dictionary<ulong, string> _tokenCache;
        private ulong _tokenHash;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvParser"/> class.
        /// </summary>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="throwException">A method which generates a new Exception</param>
        public CsvParser(CsvConfiguration configuration, Func<string, Exception> throwException)
        {
            _token = new StringBuilder();
            _tokenCache = new Dictionary<ulong, string>();
            Configuration = configuration;
            ThrowException = throwException;
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
        public CsvConfiguration Configuration { get; }

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
        /// Get current token
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            if (Configuration.CacheValues)
            {
                if (_tokenCache.TryGetValue(_tokenHash, out var cachedToken))
                {
                    return cachedToken;
                }
                else
                {
                    var newToken = _token.ToString();
                    _tokenCache.Add(_tokenHash, newToken);
                    return newToken;
                }
            }
            else
            {
                return _token.ToString();
            }
        }

        /// <summary>
        /// Parses CSV source for next token.
        /// </summary>
        /// <param name="nextChar">A method which gives back the next char 
        /// from a CSV source</param>
        /// <returns>true if token could be read; otherwise false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NextToken(Func<char> nextChar)
        {
            _token.Clear();
            _tokenHash = 3074457345618258791ul;
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
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async ValueTask<bool> NextTokenAsync(Func<ValueTask<char>> nextCharAsync)
        {
            _token.Clear();
            _tokenHash = 3074457345618258791ul;
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ResetState()
        {
            State = TokenizerState.IsSeekingStart;
        }

        /// <summary>
        /// Appends given character to current token.
        /// </summary>
        /// <param name="c">The character</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AppendToToken(char c)
        {
            if (Configuration.CacheValues)
            {
                unchecked
                {
                    _tokenHash += c;
                    _tokenHash *= 3074457345618258799ul;
                }
            }
            _token.Append(c);
        }

        /// <summary>
        /// Categorise a character.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <returns>Category of the character.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
                                AppendToToken(c);
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
                                AppendToToken(c);
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
                                AppendToToken(c);
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
                                AppendToToken(c);
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
                                AppendToToken(c);
                                return TokenizerWorkflow.Continue;
                            case CharCategory.IsWhitespace:
                                AppendToToken(' ');
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
                                AppendToToken(c);
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
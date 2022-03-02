﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eCommerce.Shared.Attributes
{
    public abstract class OutputProcessorActionFilterAttribute : ActionFilterAttribute
    {
        public OutputProcessorActionFilterAttribute()
        {
            InputEncoding = Encoding.UTF8;
            OutputEncoding = Encoding.UTF8;
        }

        /// <summary>Gets or sets the input encoding.</summary>
        public Encoding InputEncoding { get; set; }

        /// <summary>Gets or sets the output encoding.</summary>
        public Encoding OutputEncoding { get; set; }

        /// <summary>Processes the output data.</summary>
        /// <param name="data">The data.</param>
        /// <returns>The processed data.</returns>
        protected abstract string Process(string data);

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var response = filterContext.HttpContext.Response;

            if (response.Filter == null) return;

            response.Filter = new OutputProcessorStream(response.Filter, InputEncoding, OutputEncoding, Process);
        }

        internal class OutputProcessorStream : Stream
        {
            private readonly StringBuilder _data = new StringBuilder();

            private readonly Stream _stream;
            private readonly Func<string, string> _processor;

            private readonly Encoding _inputEncoding;
            private readonly Encoding _outputEncoding;

            public OutputProcessorStream(Stream stream, Encoding inputEncoding, Encoding outputEncoding, Func<string, string> processor)
            {
                _stream = stream;
                _processor = processor;
                _inputEncoding = inputEncoding;
                _outputEncoding = outputEncoding;
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                _data.Append(_inputEncoding.GetString(buffer, offset, count));
            }

            /// <exception cref="IOException">An I/O error has occurred.</exception>
            /// <exception cref="Exception">A delegate callback throws an exception.</exception>
            public override void Close()
            {
                var output = _outputEncoding.GetBytes(_processor(_data.ToString()));
                _stream.Write(output, 0, output.Length);
                _stream.Flush();
                _data.Clear();
            }

            public override void Flush()
            {
            }

            /// <exception cref="IOException">An I/O error occurs. </exception>
            public override int Read(byte[] buffer, int offset, int count)
            {
                return _stream.Read(buffer, offset, count);
            }

            /// <exception cref="IOException">An I/O error occurs. </exception>
            public override long Seek(long offset, SeekOrigin origin)
            {
                return _stream.Seek(offset, origin);
            }

            /// <exception cref="IOException">An I/O error occurs. </exception>
            public override void SetLength(long value)
            {
                _stream.SetLength(value);
            }

            public override bool CanRead { get { return true; } }

            public override bool CanSeek { get { return true; } }

            public override bool CanWrite { get { return true; } }

            public override long Length { get { return 0; } }

            public override long Position { get; set; }
        }
    }
}
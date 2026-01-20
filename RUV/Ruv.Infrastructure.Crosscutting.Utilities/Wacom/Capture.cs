using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WintabDN;

namespace Ruv.Infrastructure.Crosscutting.Utilities.Wacom
{
    public class Capture : IDisposable
    {
        #region Attributes

        private CWintabContext m_logContext = null;
        private CWintabData m_wtData = null;
        private uint m_maxPkts = 1;   // max num pkts to capture/display at a time

        private int m_minX = 0;
        private int m_pkX = 0;
        private int m_pkY = 0;
        private uint m_pressure = 0;
        private uint m_pkTime = 0;
        private uint m_pkTimeLast = 0;
        int m_clientWidth = 0;
        int m_clientHeight = 0;

        private Point m_lastPoint = Point.Empty;
        private Pen m_pen;
        private Pen m_backPen;
        
        private const int m_TABEXTX = 10000;
        private const int m_TABEXTY = 10000;

        #endregion
        #region Events

        public event EventHandler<LineEventArgs> DrawLine;
        public event EventHandler<RectangleEventArgs> DrawRectangle;

        #endregion
        #region Properties

        public Point MinPt { get; private set; }
        public Point MaxPt { get; private set; }

        #region Private

        #endregion

        #region Protected

        #endregion

        #region Public

        #endregion

        #endregion
        #region Methods

        #region Private

        private void Enable_Scribble(bool enable = false)
        {
            if (enable)
            {
                // Set up to capture 1 packet at a time.
                m_maxPkts = 1;

                m_pen = new Pen(0x000000);
                m_backPen = new Pen(0xFFFFFF);
            }
            else
            {
                // Remove scribble context.
                CloseCurrentContext();
            }
        }

        private void InitDataCapture(int ctxHeight_I = m_TABEXTX, int ctxWidth_I = m_TABEXTY, bool ctrlSysCursor_I = true)
        {
            try
            {
                // Close context from any previous test.
                CloseCurrentContext();

                //TraceMsg("Opening context...\n");

                m_logContext = OpenTestDigitizerContext(ctxWidth_I, ctxHeight_I, ctrlSysCursor_I);

                if (m_logContext == null)
                {
                    //TraceMsg("Test_DataPacketQueueSize: FAILED OpenTestDigitizerContext - bailing out...\n");
                    return;
                }

                // Create a data object and set its WT_PACKET handler.
                m_wtData = new CWintabData(m_logContext);
                m_wtData.SetWTPacketEventHandler(WTPacketEventHandler);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void CloseCurrentContext()
        {
            try
            {
                if (m_logContext != null)
                {
                    // Set system cursor before release the resources
                    m_logContext.Options |= (uint)ECTXOptionValues.CXO_SYSTEM;
                    m_logContext.Close();
                    m_logContext = null;
                    m_wtData = null;
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private CWintabContext OpenTestDigitizerContext(int width_I = m_TABEXTX, int height_I = m_TABEXTY, bool ctrlSysCursor = true)
        {
            bool status = false;
            CWintabContext logContext = null;

            try
            {
                // Get the default digitizing context.
                // Default is to receive data events.
                logContext = CWintabInfo.GetDefaultDigitizingContext(ECTXOptionValues.CXO_MESSAGES);

                // Set system cursor if caller wants it.
                if (ctrlSysCursor)
                {
                    logContext.Options |= (uint)ECTXOptionValues.CXO_SYSTEM;
                }

                if (logContext == null)
                {
                    return null;
                }

                // Modify the digitizing region.
                logContext.Name = "WintabDN Event Data Context";

                // output in a 10000 x 10000 grid
                logContext.OutOrgX = logContext.OutOrgY = 0;
                logContext.OutExtX = width_I;
                logContext.OutExtY = height_I;


                // Open the context, which will also tell Wintab to send data packets.
                status = logContext.Open();

                //TraceMsg("Context Open: " + (status ? "PASSED [ctx=" + logContext.HCtx + "]" : "FAILED") + "\n");
            }
            catch (Exception ex)
            {
                //TraceMsg("OpenTestDigitizerContext ERROR: " + ex.ToString());
            }

            return logContext;
        }

        #endregion

        #region Protected

        /// <summary>
        /// Called when Wintab WT_PACKET events are received.
        /// </summary>
        /// <param name="sender_I">The EventMessage object sending the report.</param>
        /// <param name="eventArgs_I">eventArgs_I.Message.WParam contains ID of packet containing the data.</param>
        protected void WTPacketEventHandler(Object sender_I, MessageReceivedEventArgs eventArgs_I)
        {
            if (m_wtData == null)
            {
                return;
            }

            try
            {
                if (m_maxPkts == 1)
                {
                    uint pktID = (uint)eventArgs_I.Message.WParam;
                    WintabPacket pkt = m_wtData.GetDataPacket(pktID);

                    if (pkt.pkContext != 0)
                    {
                        m_pkX = pkt.pkX;
                        m_pkY = pkt.pkY;
                        m_pressure = pkt.pkNormalPressure.pkAbsoluteNormalPressure;

                        m_pkTime = pkt.pkTime;

                        int X = (int)((double)(m_pkX * m_clientWidth) / (double)m_TABEXTX);
                        int Y = (int)((double)m_clientHeight - ((double)(m_pkY * m_clientHeight) / (double)m_TABEXTY));

                        Point tabPoint = new Point(X, Y);

                        if (m_lastPoint.Equals(Point.Empty))
                        {
                            m_lastPoint = tabPoint;
                            m_pkTimeLast = m_pkTime;
                            MinPt = m_lastPoint;
                            MaxPt = m_lastPoint;
                        }

                        m_pen.Width = (float)(m_pressure / 200);
                        if (m_pressure > 0)
                        {
                            if (m_pkTime - m_pkTimeLast < 5)
                            {
                                OnDrawRectangle(new RectangleEventArgs { MPen = m_pen, X = X, Y = Y, Width = 1, Height = 1 });
                            }
                            else
                            {
                                OnDrawLine(new LineEventArgs { MPen = m_pen, Point1 = tabPoint, Point2 = m_lastPoint });
                            }
                        }

                        m_lastPoint = tabPoint;
                        m_pkTimeLast = m_pkTime;
                        MinPt = MinPt < m_lastPoint;
                        MaxPt = MaxPt > m_lastPoint;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("FAILED to get packet data: " + ex.ToString());
            }
        }

        protected void OnDrawLine(LineEventArgs e)
        {
            if (DrawLine != null)
            {
                DrawLine(this, e);
            }
        }

        protected void OnDrawRectangle(RectangleEventArgs e)
        {
            if (DrawRectangle != null)
            {
                DrawRectangle(this, e);
            }
        }

        #endregion

        #region Public

        public void Enable(int width, int height)
        {
            m_clientWidth = width;
            m_clientHeight = height;

            Enable_Scribble(true);
            InitDataCapture(m_TABEXTX, m_TABEXTY, false);
        }

        public void Disable()
        {
            m_clientWidth = 0;
            m_clientHeight = 0;

            Enable_Scribble();
        }

        #region Interface implementation

        public void Dispose()
        {
            CloseCurrentContext();
        }

        #endregion

        #endregion

        #endregion
    }
}

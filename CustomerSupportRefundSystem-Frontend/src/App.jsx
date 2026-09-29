import { useEffect, useState } from "react";
import "./App.css";

const API_URL =
  import.meta.env.VITE_API_URL || "http://localhost:5149/api";

function App() {
  const [orderId, setOrderId] = useState("");
  const [message, setMessage] = useState("");
  const [result, setResult] = useState(null);
  const [requests, setRequests] = useState([]);
  const [loading, setLoading] = useState(false);
  const [loadingRequests, setLoadingRequests] = useState(false);
  const [error, setError] = useState("");

  const loadRequests = async () => {
    try {
      setLoadingRequests(true);

      const response = await fetch(`${API_URL}/refunds`);

      if (!response.ok) {
        throw new Error("Failed to load refund requests.");
      }

      const data = await response.json();
      setRequests(data);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoadingRequests(false);
    }
  };

  useEffect(() => {
    loadRequests();
  }, []);

  const submitRefund = async (e) => {
    e.preventDefault();

    if (!orderId || !message.trim()) {
      setError("Please enter an order ID and describe your refund request.");
      return;
    }

    try {
      setLoading(true);
      setError("");
      setResult(null);

      const response = await fetch(`${API_URL}/refunds`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          orderId: Number(orderId),
          customerMessage: message,
        }),
      });

      const data = await response.json();

      if (!response.ok) {
        throw new Error(data.message || "Failed to submit refund request.");
      }

      setResult(data);
      setOrderId("");
      setMessage("");

      await loadRequests();
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  const getDecisionClass = (decision) => {
    switch (decision) {
      case "Approved":
        return "approved";
      case "Denied":
        return "denied";
      case "Escalated":
        return "escalated";
      default:
        return "";
    }
  };

  return (
    <div className="app">
      <header className="header">
        <div>
          <h1>RefundAssist</h1>
          <p>AI-powered customer refund support</p>
        </div>
      </header>

      <main className="container">
        <section className="customer-section">
          <div className="section-heading">
            <span className="eyebrow">CUSTOMER SUPPORT</span>
            <h2>Request a refund</h2>
            <p>
              Tell us what happened and we'll review your refund request.
            </p>
          </div>

          <form className="refund-form" onSubmit={submitRefund}>
            <div className="form-group">
              <label htmlFor="orderId">Order ID</label>
              <input
                id="orderId"
                type="number"
                placeholder="e.g. 1"
                value={orderId}
                onChange={(e) => setOrderId(e.target.value)}
              />
              <small>
                Try orders 1–19 from the demo dataset.
              </small>
            </div>

            <div className="form-group">
              <label htmlFor="message">What's wrong with your order?</label>
              <textarea
                id="message"
                rows="5"
                placeholder="Example: My headphones arrived damaged and I would like a refund."
                value={message}
                onChange={(e) => setMessage(e.target.value)}
              />
            </div>

            <button type="submit" disabled={loading}>
              {loading ? "Reviewing request..." : "Submit refund request"}
            </button>
          </form>

          {error && (
            <div className="error-message">
              {error}
            </div>
          )}

          {result && (
            <div className="result-card">
              <div className="result-header">
                <div>
                  <span className="eyebrow">REFUND REQUEST #{result.id}</span>
                  <h3>{result.productName}</h3>
                </div>

                <span
                  className={`decision ${getDecisionClass(result.decision)}`}
                >
                  {result.decision}
                </span>
              </div>

              <div className="result-details">
                <div>
                  <span>Customer</span>
                  <strong>{result.customerName}</strong>
                </div>

                <div>
                  <span>Order</span>
                  <strong>#{result.orderId}</strong>
                </div>

                <div>
                  <span>Amount</span>
                  <strong>${Number(result.amount).toFixed(2)}</strong>
                </div>
              </div>

              <div className="reason">
                <h4>Decision reason</h4>
                <p>{result.decisionReason}</p>
              </div>

              {result.aiResponse && (
                <div className="ai-response">
                  <div className="ai-label">
                    <span>✦</span>
                    AI Support Assistant
                  </div>
                  <p>{result.aiResponse}</p>
                </div>
              )}
            </div>
          )}
        </section>

        <section className="dashboard-section">
          <div className="section-heading dashboard-heading">
            <div>
              <span className="eyebrow">ADMIN</span>
              <h2>Refund dashboard</h2>
              <p>Recent customer refund requests and outcomes.</p>
            </div>

            <button
              className="refresh-button"
              onClick={loadRequests}
              disabled={loadingRequests}
            >
              {loadingRequests ? "Refreshing..." : "Refresh"}
            </button>
          </div>

          <div className="table-container">
            {requests.length === 0 ? (
              <div className="empty-state">
                No refund requests yet.
              </div>
            ) : (
              <table>
                <thead>
                  <tr>
                    <th>Customer</th>
                    <th>Order</th>
                    <th>Product</th>
                    <th>Amount</th>
                    <th>Decision</th>
                    <th>Reason</th>
                  </tr>
                </thead>

                <tbody>
                  {requests.map((request) => (
                    <tr key={request.id}>
                      <td>{request.customerName}</td>
                      <td>#{request.orderId}</td>
                      <td>{request.productName}</td>
                      <td>${Number(request.amount).toFixed(2)}</td>
                      <td>
                        <span
                          className={`decision ${getDecisionClass(
                            request.decision
                          )}`}
                        >
                          {request.decision}
                        </span>
                      </td>
                      <td>{request.decisionReason}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            )}
          </div>
        </section>
      </main>
    </div>
  );
}

export default App;
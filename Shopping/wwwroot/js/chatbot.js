// AI Chatbot functionality
(function() {
    const chatbotToggle = document.getElementById('chatbotToggle');
    const chatbotWindow = document.getElementById('chatbotWindow');
    const chatbotMessages = document.getElementById('chatbotMessages');
    const chatbotInput = document.getElementById('chatbotInput');
    const chatbotSend = document.getElementById('chatbotSend');
    const quickActions = document.querySelectorAll('.quick-action-btn');

    let isOpen = false;

    // Toggle chatbot
    chatbotToggle.addEventListener('click', () => {
        isOpen = !isOpen;
        chatbotWindow.classList.toggle('active');
        chatbotToggle.classList.toggle('active');
        
        if (isOpen) {
            chatbotInput.focus();
        }
    });

    // Send message
    async function sendMessage(message) {
        if (!message.trim()) return;

        // Add user message
        addMessage(message, 'user');
        chatbotInput.value = '';

        // Show typing indicator
        showTypingIndicator();

        try {
            const response = await fetch('/api/Chatbot/message', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ message: message })
            });

            // Remove typing indicator
            removeTypingIndicator();

            if (response.ok) {
                const data = await response.json();
                addMessage(data.response, 'bot');
            } else {
                const errorText = await response.text();
                console.error('API Error Response:', errorText);
                addMessage('Xin lỗi, có lỗi xảy ra. Vui lòng thử lại!', 'bot');
            }
        } catch (error) {
            removeTypingIndicator();
            console.error('Error:', error);
            addMessage('Xin lỗi, không thể kết nối đến server. Vui lòng thử lại sau!', 'bot');
        }
    }

    // Add message to chat
    function addMessage(text, sender) {
        const messageDiv = document.createElement('div');
        messageDiv.className = `message ${sender}`;

        if (sender === 'bot') {
            messageDiv.innerHTML = `
                <div class="avatar">
                    <i class="fas fa-robot"></i>
                </div>
                <div class="text">${formatMessage(text)}</div>
            `;
        } else {
            messageDiv.innerHTML = `
                <div class="text">${text}</div>
            `;
        }

        chatbotMessages.appendChild(messageDiv);
        chatbotMessages.scrollTop = chatbotMessages.scrollHeight;
    }

    // Format message with line breaks and lists
    function formatMessage(text) {
        if (!text || typeof text !== 'string') {
            return '';
        }
        return text
            .replace(/\n/g, '<br>')
            .replace(/\*\*(.*?)\*\*/g, '<strong>$1</strong>')
            .replace(/\*(.*?)\*/g, '<em>$1</em>');
    }

    // Show typing indicator
    function showTypingIndicator() {
        const typingDiv = document.createElement('div');
        typingDiv.className = 'message typing';
        typingDiv.id = 'typing-indicator';
        typingDiv.innerHTML = `
            <div class="avatar">
                <i class="fas fa-robot"></i>
            </div>
            <div class="typing-indicator">
                <span></span>
                <span></span>
                <span></span>
            </div>
        `;
        chatbotMessages.appendChild(typingDiv);
        chatbotMessages.scrollTop = chatbotMessages.scrollHeight;
    }

    // Remove typing indicator
    function removeTypingIndicator() {
        const typingIndicator = document.getElementById('typing-indicator');
        if (typingIndicator) {
            typingIndicator.remove();
        }
    }

    // Event listeners
    chatbotSend.addEventListener('click', () => {
        sendMessage(chatbotInput.value);
    });

    chatbotInput.addEventListener('keypress', (e) => {
        if (e.key === 'Enter') {
            sendMessage(chatbotInput.value);
        }
    });

    // Quick actions
    quickActions.forEach(btn => {
        btn.addEventListener('click', () => {
            const message = btn.getAttribute('data-message');
            sendMessage(message);
        });
    });

    // Disable send button when input is empty
    chatbotInput.addEventListener('input', () => {
        chatbotSend.disabled = !chatbotInput.value.trim();
    });

    // Initial state
    chatbotSend.disabled = true;
})();

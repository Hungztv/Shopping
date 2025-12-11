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
                const botContainer = addMessage(data.response, 'bot');
                // Attempt both anchor-based and raw text parsing
                renderProductCards(botContainer);
                renderRawProductCards(botContainer);
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
        return messageDiv;
    }

    
    function formatMessage(text) {
        if (!text || typeof text !== 'string') {
            return '';
        }
        const origin = window.location.origin;
        // First escape HTML special chars except we will inject anchors later
        let html = text
            .replace(/&/g, '&amp;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;');

        // Restore line breaks and simple markdown
        html = html
            .replace(/\n/g, '<br>')
            .replace(/\*\*(.*?)\*\*/g, '<strong>$1</strong>')
            .replace(/\*(.*?)\*/g, '<em>$1</em>');

        // 1) Convert absolute product detail URLs
        html = html.replace(/https?:\/\/[\w\.\-:]+\/Product\/Details\/(\d+)/g, (full, id) => {
            return `<a href="${full}" class="chat-link" data-product-id="${id}">${full}</a>`;
        });
        // 2) Convert relative URLs not already inside anchor tags
        html = html.replace(/(^|[^"'>])(\/Product\/Details\/(\d+))/g, (full, prefix, rel, id) => {
            // If prefix ends inside an existing anchor, skip
            const absolute = `${origin}${rel}`;
            return `${prefix}<a href="${absolute}" class="chat-link" data-product-id="${id}">${absolute}</a>`;
        });

        return html;
    }

    // Extract product IDs from anchors and render product cards below the bot message
    async function renderProductCards(messageElement) {
        if (!messageElement) return;
        const links = Array.from(messageElement.querySelectorAll('a.chat-link'));
        const ids = [...new Set(links.map(a => {
            const m = a.getAttribute('href').match(/\/Product\/Details\/(\d+)/);
            return m ? m[1] : null;
        }).filter(Boolean))];
        if (!ids.length) return;
        try {
            const resp = await fetch(`/api/Chatbot/products?ids=${ids.join(',')}`);
            if (!resp.ok) return;
            const products = await resp.json();
            if (!Array.isArray(products) || !products.length) return;
            const wrapper = document.createElement('div');
            wrapper.className = 'chat-product-cards';
            products.forEach(p => {
                const card = document.createElement('div');
                card.className = 'chat-product-card';
                card.innerHTML = `
                    <a href="${p.Link}" class="chat-product-thumb">
                        <img src="${p.Image && p.Image.startsWith('http') ? p.Image : '/media/products/' + p.Image}" alt="${p.Name}">
                    </a>
                    <div class="chat-product-info">
                        <a href="${p.Link}" class="chat-product-name">${p.Name}</a>
                        <div class="chat-product-price">${Number(p.Price).toLocaleString('vi-VN')} VNĐ</div>
                        <button type="button" class="chat-product-view" data-link="${p.Link}">Xem chi tiết</button>
                    </div>
                `;
                wrapper.appendChild(card);
            });
            messageElement.appendChild(wrapper);
            injectProductCardStyles();
            enableCardNavigation(wrapper);
        } catch (e) {
            console.warn('Render product cards failed:', e);
        }
    }

    // Parse raw text (without anchors) to extract product links
    async function renderRawProductCards(messageElement) {
        if (!messageElement) return;
        const textBlock = messageElement.querySelector('.text');
        if (!textBlock) return;
        const raw = textBlock.innerText;
        const matches = [...raw.matchAll(/(?:https?:\/\/[\w\.:\-]+)?\/Product\/Details\/(\d+)/g)].map(m => m[1]);
        const ids = [...new Set(matches)];
        if (!ids.length) return; // already handled by anchor path maybe
        // Avoid duplicate rendering if cards already exist
        if (messageElement.querySelector('.chat-product-cards')) return;
        try {
            const resp = await fetch(`/api/Chatbot/products?ids=${ids.join(',')}`);
            if (!resp.ok) return;
            const products = await resp.json();
            if (!Array.isArray(products) || !products.length) return;
            const wrapper = document.createElement('div');
            wrapper.className = 'chat-product-cards';
            products.forEach(p => {
                const card = document.createElement('div');
                card.className = 'chat-product-card';
                card.innerHTML = `
                    <a href="${p.Link}" class="chat-product-thumb">
                        <img src="${p.Image && p.Image.startsWith('http') ? p.Image : '/media/products/' + p.Image}" alt="${p.Name}">
                    </a>
                    <div class="chat-product-info">
                        <a href="${p.Link}" class="chat-product-name">${p.Name}</a>
                        <div class="chat-product-price">${Number(p.Price).toLocaleString('vi-VN')} VNĐ</div>
                        <button type="button" class="chat-product-view" data-link="${p.Link}">Xem chi tiết</button>
                    </div>
                `;
                wrapper.appendChild(card);
            });
            messageElement.appendChild(wrapper);
            injectProductCardStyles();
            enableCardNavigation(wrapper);
        } catch (e) {
            console.warn('Render raw product cards failed:', e);
        }
    }

    function injectProductCardStyles() {
        if (document.getElementById('chat-product-card-styles')) return;
        const style = document.createElement('style');
        style.id = 'chat-product-card-styles';
        style.textContent = `
            .chat-product-cards { display: flex; flex-wrap: wrap; gap: 8px; margin-top: 8px; }
            .chat-product-card { display: flex; background:#fff; border:1px solid #eee; border-radius:8px; padding:6px; width:190px; box-shadow:0 1px 3px rgba(0,0,0,0.08); transition:box-shadow .2s; position:relative; }
            .chat-product-card:hover { box-shadow:0 4px 12px rgba(0,0,0,0.15); }
            .chat-product-thumb { flex-shrink:0; width:64px; height:64px; overflow:hidden; border-radius:6px; display:block; }
            .chat-product-thumb img { width:100%; height:100%; object-fit:cover; }
            .chat-product-info { display:flex; flex-direction:column; margin-left:8px; gap:4px; }
            .chat-product-name { font-size:12.5px; font-weight:600; text-decoration:none; color:#222; line-height:1.2; }
            .chat-product-name:hover { text-decoration:underline; }
            .chat-product-price { font-size:12px; font-weight:500; color:#d9534f; }
            .chat-product-view { cursor:pointer; border:none; background:#007bff; color:#fff; font-size:11px; padding:4px 6px; border-radius:4px; }
            .chat-product-view:hover { background:#0056b3; }
            a.chat-link { color:#007bff; text-decoration:none; }
            a.chat-link:hover { text-decoration:underline; }
        `;
        document.head.appendChild(style);
    }

    function enableCardNavigation(wrapper) {
        wrapper.querySelectorAll('.chat-product-view').forEach(btn => {
            btn.addEventListener('click', e => {
                const link = btn.getAttribute('data-link');
                if (link) {
                    window.location.href = link;
                }
            });
        });
        // Allow clicking entire card (except buttons) to navigate
        wrapper.querySelectorAll('.chat-product-card').forEach(card => {
            card.addEventListener('click', e => {
                if (e.target.closest('.chat-product-view')) return;
                const linkEl = card.querySelector('.chat-product-name');
                if (linkEl) window.location.href = linkEl.getAttribute('href');
            });
        });
    }

   
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

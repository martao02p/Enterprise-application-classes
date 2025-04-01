package lab.ejb;

import jakarta.ejb.ActivationConfigProperty;
import jakarta.ejb.MessageDriven;
import jakarta.jms.*;
import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import java.util.regex.Pattern;

@JMSDestinationDefinition(name = "java:app/jms/NewsQueue",
        interfaceName = "jakarta.jms.Queue", resourceAdapter = "jmsra",
        destinationName = "NewsQueue")
@MessageDriven(activationConfig = {
        @ActivationConfigProperty(propertyName = "destinationLookup", propertyValue = "java:app/jms/NewsQueue"),
        @ActivationConfigProperty(propertyName = "destinationType", propertyValue = "jakarta.jms.Queue")
})
public class NewsMDB implements MessageListener {
    @PersistenceContext
    private EntityManager em;

    @Override
    public void onMessage(Message message) {
        ObjectMessage msg = null;
        try {
//            if (message instanceof ObjectMessage) {
//                msg = (ObjectMessage) message;
//                NewsItem e = (NewsItem) msg.getObject();
//                em.persist(e);
//            }

            if (message instanceof TextMessage) {
                TextMessage txtMessage = (TextMessage) message;
                String text = txtMessage.getText();   // "naglowek|tresc"

                // rozdzielenie
                String regex = Pattern.quote("|");
                String[] parts = text.split(regex, 2);
                if (parts.length == 2) {
                    String heading = parts[0];
                    String body = parts[1];

                    NewsItem e = new NewsItem();
                    e.setHeading(heading);
                    e.setBody(body);
                    em.persist(e);
                }
            } else {
                System.out.println("Ignored non-text message: " + message);
            }
        } catch (JMSException e) {
            e.printStackTrace();
        }

    }
}

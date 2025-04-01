package lab.backing;

import jakarta.annotation.Resource;
import jakarta.enterprise.context.RequestScoped;
import jakarta.inject.Inject;
import jakarta.inject.Named;
import jakarta.jms.JMSContext;
import jakarta.jms.JMSException;
import jakarta.jms.ObjectMessage;
import lab.ejb.NewsItem;
import lab.ejb.NewsItemFacadeLocal;
import java.util.List;
import jakarta.jms.TextMessage;


@Named
@RequestScoped
public class NewsBean {
    @Inject
    private NewsItemFacadeLocal facade;

    @Inject
    private JMSContext context;
    @Resource(lookup="java:app/jms/NewsQueue")
    private jakarta.jms.Queue queue;


    private String headingText;
    private String bodyText;

    public String getHeadingText() {
        return headingText;
    }
    public void setHeadingText(String headingText) {
        this.headingText = headingText;
    }

    public String getBodyText() {
        return bodyText;
    }
    public void setBodyText(String bodyText) {
        this.bodyText = bodyText;
    }


    private void sendNewsItem(String heading, String body) {
//        try {
//            ObjectMessage message = context.createObjectMessage();
//            NewsItem e = new NewsItem();
//            e.setHeading(heading);
//            e.setBody(body);
//            message.setObject(e);

            String text = heading + "|" + body;
            TextMessage message = context.createTextMessage(text);

            context.createProducer().send(queue, message);
//        }
//        catch (JMSException ex) {
//            ex.printStackTrace();
//        }
    }
    public List<NewsItem> getNewsItems() {
        return facade.getAllNewsItems();
    }

    public String submitNews() {
        sendNewsItem(headingText, bodyText);
        headingText = "";
        bodyText = "";
        return null;
    }
}

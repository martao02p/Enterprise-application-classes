package lab.services;

import jakarta.enterprise.context.ApplicationScoped;
import jakarta.inject.Inject;
import jakarta.transaction.Transactional;
import lab.data.ComplaintRepository;
import lab.dto.ComplaintDTO;
import lab.entities.Complaint;
import org.modelmapper.ModelMapper;
import org.modelmapper.TypeToken;

import java.lang.reflect.Type;
import java.util.List;

@ApplicationScoped
public class ComplaintService {

    @Inject
    private ComplaintRepository repository;

    @Inject
    private ModelMapper mapper;

    @Transactional
    public void create(ComplaintDTO dto) {
        repository.create(mapper.map(dto, Complaint.class));
    }

    @Transactional
    public void edit(Long id, ComplaintDTO dto) {
        Complaint entity = mapper.map(dto, Complaint.class);
        entity.setId(id);
        repository.edit(entity);
    }

    @Transactional
    public void remove(Long id) {
        Complaint entity = repository.find(id);
        repository.remove(entity);
    }

    @Transactional
    public ComplaintDTO find(Long id) {
        Complaint entity = repository.find(id);
        return mapper.map(entity, ComplaintDTO.class);
    }

    @Transactional
    public List<ComplaintDTO> findAll(String status) {
        List<Complaint> entityList = repository.findAll(status);
        Type listType = new TypeToken<List<ComplaintDTO>>() {}.getType();
        return mapper.map(entityList, listType);
    }
}
